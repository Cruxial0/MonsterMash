using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using _Scripts.Interfaces;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
using Newtonsoft.Json;
>>>>>>> Stashed changes
using UnityEngine;
=======
<<<<<<< HEAD

=======
using UnityEngine;
>>>>>>> c79e3e564050be67919d620e8e6a3f0dad715a09
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            File.WriteAllText(filePath, JsonUtility.ToJson(this));
=======
<<<<<<< HEAD
            
=======
            File.WriteAllText(filePath, JsonUtility.ToJson(this));
>>>>>>> c79e3e564050be67919d620e8e6a3f0dad715a09
>>>>>>> Stashed changes
=======
            if (!File.Exists(FilePath)) File.Create(FilePath);
            Debug.Log(FilePath);
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(this.UnlockedLevels, Formatting.Indented));
>>>>>>> Stashed changes
        }

        public void LoadLevels()
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
            
=======
>>>>>>> Stashed changes
            UnlockedLevels = JsonUtility.FromJson<List<LevelSave>>(File.ReadAllText(filePath));
=======
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
                
>>>>>>> Stashed changes
            UnpackLevels();
        }

        private void UnpackLevels()
        {
            if(UnlockedLevels.Count == 0) return;
            foreach (var levelSave in UnlockedLevels)
            {
                LevelStarRatings.Add(levelSave.SceneName, levelSave.StarCount);
            }
<<<<<<< Updated upstream
=======
>>>>>>> c79e3e564050be67919d620e8e6a3f0dad715a09
>>>>>>> Stashed changes
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