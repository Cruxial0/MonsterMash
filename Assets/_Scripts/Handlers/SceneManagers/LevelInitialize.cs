using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    /// <summary>
    /// A collection of all objects in the scene.
    /// </summary>
    public class LevelInitialize
    {
        
    }

    /// <summary>
    /// Level Object
    /// </summary>
    public class Level
    {
        public string LevelName { get; set; }
        public string SceneName { get; }
        public Scene LevelScene { get; set; }

        public Level(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}