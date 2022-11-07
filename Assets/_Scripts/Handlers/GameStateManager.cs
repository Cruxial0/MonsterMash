using System.Linq;
using _Scripts.GUI.PostLevelScreens;
using _Scripts.MonoBehaviour.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _Scripts.Handlers
{
    public class GameStateManager : UnityEngine.MonoBehaviour
    {
        private PlayerInteractionHandler _handler; //Instance of PlayerInteractionHandler
        private GameObject _player; //Instance of player
        private float currTime; //Current time
        private GameStateScreen gameScreens = new GameStateScreen();

        private float delay = 5; //Transition delay
        private bool lost = false; //Lost?
        private bool postLost;
        private bool _enabled = true;

        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player; //Assign player
            _handler = handler; //Assign value
        }

        private void Awake()
        {
            delay = 3f; //Set delay
            currTime = 0f;
            _enabled = true;
        }

        private void Update()
        {
            if(!_enabled) return;

            currTime += Time.deltaTime; //Increment time

            if (currTime > delay)
            {
                if (lost)
                {
                    //Load MainMenu
                    var scene = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(scene);
                    _enabled = false;
                    return;
                }

                //Switch on Scene names
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Level0":
                        SceneManager.LoadScene("Level1");
                        return;
                    case "Level1":
                        SceneManager.LoadScene("Level2");
                        return;
                    case "Level2":
                        SceneManager.LoadScene("Level3");
                        return;
                }

                SceneManager.LoadScene("MenuTest");
            }
        }

        public void Lose(LoseCondition loseCondition)
        {
            var go = new GameObject(); //Add empty handler
            var manager = go.AddComponent<GameStateManager>(); //Add GameStateManager component to object

            manager.lost = true;

            //Destroy player
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerState = PlayerState.Dead;
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            //Set text color to red
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.red;
            //Stop timer
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            //Disable camera script
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;

            //Instantiate loss screen
            Instantiate(gameScreens.RestartLevelScreen());
        }

        public void Win(float timeLeft)
        {
            //Destroy player
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            //Set text color to green
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.green;
            //Stop timer
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            //Disable camera script
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;
            //Instantiate win screen
            Instantiate(gameScreens.WinLevelScreen());

            var go = new GameObject(); //Add empty handler
            go.AddComponent<GameStateManager>(); //Add GameStateManager component to object

            var level = LevelManager.GetAllScenes().First(x => x.Level.SceneName == SceneManager.GetActiveScene().name);
            
            int stars = 0;
            if (level.StarLevels.OneStarRequirement <= timeLeft) stars++;
            if (level.StarLevels.TwoStarRequirement <= timeLeft) stars++;
            if (level.StarLevels.ThreeStarRequirement <= timeLeft) stars++;
            if (PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script.slider.value >=
                level.StarLevels.NoiseThreshold && stars != 0) stars--;
        }
    }
    
    public enum LoseCondition
    {
        Time,
        Trap,
        Noise,
        Parents
    }
}