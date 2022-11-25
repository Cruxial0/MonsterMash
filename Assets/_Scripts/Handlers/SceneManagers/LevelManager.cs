using System;
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
                .Replace(@"\Library\ScriptAssemblies", String.Empty)
                .Replace(@"/Library/ScriptAssemblies", String.Empty));
            FilePath = Application.isEditor ? Path.Combine(startupPath, "Assets/Resources", "LevelSave.json") : Path.Combine(Application.persistentDataPath, "LevelSave.json");
        }
        
        public void AddLevel(string level, int starCount)
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);

            if (UnlockedLevels.Count(x => x.SceneName == level) > 0)
            {
                var instance = UnlockedLevels.First(x => x.SceneName == level);
                if (instance.StarCount >= starCount) return;
                instance.StarCount = starCount;
                LevelStarRatings[level] = starCount;
                return;
            }
            
            if(UnlockedLevels.Count(x => x.SceneName == level) == 0)
                UnlockedLevels.Add(new LevelSave(level, starCount));
            if(!LevelStarRatings.ContainsKey(level))
                LevelStarRatings.Add(level, starCount);
        }
        
        public void SaveLevels()
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(this.UnlockedLevels, Formatting.Indented));
        }

        public void LoadLevels()
        {
            if (!File.Exists(FilePath)) File.Create(FilePath);
            if(File.ReadAllText(FilePath) != String.Empty)
                try
                {
                    UnlockedLevels = JsonConvert.DeserializeObject<List<LevelSave>>(File.ReadAllText(FilePath)) as List<LevelSave>;
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
        public string SceneName { get; set; }
        public int StarCount { get; set; }
        
        public LevelSave(string levelName, int starCount)
        {
            this.SceneName = levelName;
            this.StarCount = starCount;
        }
    }
}