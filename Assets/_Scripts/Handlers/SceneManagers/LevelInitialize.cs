using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    /// <summary>
    ///     A collection of all objects in the scene.
    /// </summary>
    public class LevelInitialize
    {
    }

    /// <summary>
    ///     Level Object
    /// </summary>
    [Serializable]
    public class Level
    {
        public Level(string sceneName)
        {
            SceneName = sceneName;
        }

        public string LevelName { get; set; }
        public string SceneName { get; } 
        public Scene LevelScene { get; set; }
    }
}