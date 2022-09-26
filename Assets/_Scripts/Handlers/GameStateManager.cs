using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class GameStateManager : UnityEngine.MonoBehaviour
    {
        private GameObject _player;
        private PlayerInteractionHandler _handler;
        
        private float delay = 5;
        private float currTime = 0f;
        private bool lost = false;
        
        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player;
            _handler = handler;
        }

        public void Lose()
        {
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.red;
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;

            Debug.Log("You lose");
            
            var text = Object.Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);

            var textObjects = text.GetComponentsInChildren<TextMeshProUGUI>();

            textObjects[0].text = "You lost!";
            textObjects[0].color = Color.red;
            textObjects[1].color = Color.red;

            lost = true;

            GameObject go = new GameObject();
            go.AddComponent<GameStateManager>();
        }
        
        public void Win()
        {
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.green;
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;
            Object.Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);
            
            GameObject go = new GameObject();
            go.AddComponent<GameStateManager>();
        }

        private void Awake()
        {
            delay = 3f;
        }

        private void Update()
        {
            currTime += Time.deltaTime;
            print(delay);
            print(currTime);
            
            if (currTime >= delay)
            {
                if (lost)
                {
                    SceneManager.LoadScene("MenuTest");
                    return;
                }
                
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

                Debug.Log("You win");
            }
        }
    }
}