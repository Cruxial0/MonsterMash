using System;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers.SceneManagers
{
    public class InitializeLevelService : UnityEngine.MonoBehaviour
    {
        public string CurrentLevel;
        public static Levels levels = new Levels();

        private void Start()
        {
            CurrentLevel = SceneManager.GetActiveScene().name;
            levels.InitPath();
            levels.LoadLevels();

            SceneManager.sceneLoaded += (arg0, mode) => CurrentLevel = arg0.name;
            
            DontDestroyOnLoad(this.gameObject);
        }
    }
}