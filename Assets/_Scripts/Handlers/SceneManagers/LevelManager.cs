using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.Interfaces;
<<<<<<< Updated upstream
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
        private readonly string filePath = @"Config/LevelSave.json";
        public List<LevelSave> UnlockedLevels { get; set; }
        public Dictionary<string, int> LevelStarRatings { get; set; }
        
        public void SaveLevels()
        {
<<<<<<< Updated upstream
            File.WriteAllText(filePath, JsonUtility.ToJson(this));
=======
<<<<<<< HEAD
            
=======
            File.WriteAllText(filePath, JsonUtility.ToJson(this));
>>>>>>> c79e3e564050be67919d620e8e6a3f0dad715a09
>>>>>>> Stashed changes
        }

        public void LoadLevels()
        {
<<<<<<< Updated upstream
=======
<<<<<<< HEAD
            
=======
>>>>>>> Stashed changes
            UnlockedLevels = JsonUtility.FromJson<List<LevelSave>>(File.ReadAllText(filePath));
            UnpackLevels();
        }

        private void UnpackLevels()
        {
            foreach (var levelSave in UnlockedLevels)
            {
                LevelStarRatings.Add(levelSave.Level.LevelName, levelSave.StarCount);
            }
<<<<<<< Updated upstream
=======
>>>>>>> c79e3e564050be67919d620e8e6a3f0dad715a09
>>>>>>> Stashed changes
        }
    }

    [Serializable]
    public class LevelSave
    {
        public Level Level { get; set; }
        public int StarCount { get; set; }

        public LevelSave(Level level, int starCount)
        {
            this.Level = level;
            this.StarCount = starCount;
        }
    }
}