using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    public static class LevelManager
    {
        public static ILevel SelectedLevel;
        public static List<ILevel> GetAllScenes()
        {
            List<ILevel> Levels = new List<ILevel>();
            
            var type = typeof(ILevel);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToArray();

            foreach (var level in types)
            {
                if (!level.IsClass) continue;
                Levels.Add((ILevel)Activator.CreateInstance(level));
            }
            
            return Levels;
        }
        
        public static void LoadScene(ILevel level)
        {
            SelectedLevel = level;
            SceneManager.LoadSceneAsync(level.Level.SceneName).completed += delegate(AsyncOperation operation)
            {
                level.Level.LevelScene = SceneManager.GetActiveScene();
            };
            
        }
    }
}