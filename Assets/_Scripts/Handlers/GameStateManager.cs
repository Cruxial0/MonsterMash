using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    public class GameStateManager
    {
        private GameObject _player;
        private PlayerInteractionHandler _handler;
        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player;
            _handler = handler;
        }

        public async void Lose()
        {
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.red;
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            Debug.Log("You lose");
            
            var text = Object.Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);

            var textObjects = text.GetComponentsInChildren<TextMeshProUGUI>();

            textObjects[0].text = "You lost!";
            textObjects[0].color = Color.red;
            textObjects[1].color = Color.red;
            
            if(Application.platform != RuntimePlatform.WebGLPlayer)
                await Task.Delay(5000);
            
            SceneManager.LoadSceneAsync("MenuTest");
            return;
        }
        
        public async void Win()
        {
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.green;
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            Object.Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);

            if(Application.platform != RuntimePlatform.WebGLPlayer)
                await Task.Delay(5000);

            SceneManager.LoadScene("MenuTest");

            Debug.Log("You win");
        }
    }
}