using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.GUI.PostLevelScreens;
using _Scripts.Interfaces;
using _Scripts.MonoBehaviour.Player;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
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

        private Dictionary<LoseCondition, UnityAction> animations = new();

        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player; //Assign player
            _handler = handler; //Assign value
        }

        private void AssignDict()
        {
            var script = PlayerInteractionHandler.SceneObjects.Player.AnimScript;
            animations = new Dictionary<LoseCondition, UnityAction>();
            animations.Add(LoseCondition.Trap, script.BearTrapAnim);
            animations[LoseCondition.Trap] = script.BearTrapAnim;
            animations[LoseCondition.Noise] = script.deathAnim;
            animations[LoseCondition.Time] = script.deathAnim;
        }
        
        private void Awake()
        {
            
            delay = 3f; //Set delay
            currTime = 0f;
            _enabled = true;
        }

        private float currAnimTime = 0f;
        public float AnimTime = 0f;
        private int i = 0;
        
        private void Update()
        {
            currAnimTime += Time.deltaTime;
            if(currAnimTime < AnimTime) return;

            if (i == 0) GenerateLossScreen();
            i++;
            
            if(!_enabled) return;

            currTime += Time.deltaTime; //Increment time

            if (currTime > delay)
            {
                var scene = SceneManager.GetActiveScene();

                if (lost)
                {
                    SceneManager.LoadScene(scene.buildIndex);
                    return;
                }
                
                switch (scene.name)
                {
                    case "LVL0":
                        SceneManager.LoadScene("LVL1");
                        return;
                    case "LVL1":
                        SceneManager.LoadScene("LVL2");
                        return;
                    case "LVL2":
                        SceneManager.LoadScene("LVL3");
                        return;
                    case "LVL3":
                        SceneManager.LoadScene("LVL4");
                        return;
                    case "LVL4":
                        SceneManager.LoadScene("LVL5");
                        return;
                    case "LVL5":
                        SceneManager.LoadScene("LVL6");
                        return;
                }  

                SceneManager.LoadScene("MenuTest");
            }
        }

        public void Lose(LoseCondition loseCondition)
        {
            var controller = PlayerInteractionHandler.SceneObjects.Player.AnimScript;
            
            var go = new GameObject(); //Add empty handler
            var manager = go.AddComponent<GameStateManager>(); //Add GameStateManager component to object

            manager.lost = true;

            switch (loseCondition)
            {
                case LoseCondition.Time:
                    manager.AnimTime = 5.1f;
                    break;
                default:
                    manager.AnimTime = 2.5f;
                    break;
            }

            animations.Add(LoseCondition.Trap, controller.BearTrapAnim);
            animations.Add(LoseCondition.Noise, controller.deathAnim);
            animations.Add(LoseCondition.Time, controller.TimeAnim);

            animations[loseCondition].Invoke();
        }

        private void GenerateLossScreen()
        {
            //Destroy player
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerState = PlayerState.Dead;
            //PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            //Set text color to red
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.red;
            //Stop timer
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            //Disable camera script
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;
            gameScreens = new GameStateScreen();
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
                level.StarLevels.NoiseThreshold && stars != 0)
                stars--;


            print(stars);
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