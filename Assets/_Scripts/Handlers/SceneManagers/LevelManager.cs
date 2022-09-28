using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;
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
}