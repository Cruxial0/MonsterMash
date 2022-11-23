﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using _Scripts.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    public static class LevelManager
    {
        public static ILevel SelectedLevel; //Level selected in LevelList

        public static List<ILevel> GetAllScenes()
        {
            //List of levels
            var Levels = new List<ILevel>();

            var type = typeof(ILevel); //type of ILevel
            //Get all instances of type ILevel in the project
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToArray();

            //Add instances of level to Levels
            foreach (var level in types)
            {
                if (!level.IsClass) continue;
                Levels.Add((ILevel)Activator.CreateInstance(level));
            }

            return Levels; //Return levels
        }

        public static void LoadScene(ILevel level)
        {
            SelectedLevel = level; //Set selected level
            //Load scene
            SceneManager.LoadSceneAsync(level.Level.SceneName).completed += delegate
            {
                level.Level.LevelScene = SceneManager.GetActiveScene();
            };
        }
    }
    
    public class Levels
    {
        private string FilePath { get; set; }
        public List<LevelSave> UnlockedLevels = new List<LevelSave>();
        public Dictionary<string, int> LevelStarRatings = new Dictionary<string, int>();

        public void InitPath()
        {
            string startupPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                .Replace("/Library/ScriptAssemblies", String.Empty));    
            FilePath = Application.isEditor ? Path.Combine(startupPath, "Assets/Resources", "LevelSave.json") : Path.Combine(Application.persistentDataPath, "LevelSave.json");
        }
        
        public void AddLevel(Level level, int starCount)
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);
            UnlockedLevels.Add(new LevelSave(level, starCount));
            LevelStarRatings.Add(level.SceneName, starCount);
        }
        
        public void SaveLevels()
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);
            Debug.Log(FilePath);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(this.UnlockedLevels, Formatting.Indented));
        }

        public void LoadLevels()
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);
            if(File.ReadAllText(FilePath) != String.Empty)
                try
                {
                    Debug.Log(File.ReadAllText(FilePath));
                    UnlockedLevels = (JsonConvert.DeserializeObject<LevelSave[]>(FilePath) ?? Array.Empty<LevelSave>()).ToList();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            
            UnpackLevels();
        }

        private void UnpackLevels()
        {
            if(UnlockedLevels.Count == 0) return;
            foreach (var levelSave in UnlockedLevels)
            {
                LevelStarRatings.Add(levelSave.SceneName, levelSave.StarCount);
            }
        }
    }
    
    public class LevelSave
    {
        [JsonProperty]
        public string SceneName { get; set; }
        [JsonProperty]
        public int StarCount { get; set; }
        
        public LevelSave(Level level, int starCount)
        {
            this.SceneName = level.SceneName;
            this.StarCount = starCount;
        }
    }
}