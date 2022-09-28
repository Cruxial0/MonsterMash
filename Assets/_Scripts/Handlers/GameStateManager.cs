using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers
{
    public class GameStateManager : UnityEngine.MonoBehaviour
    {
        private PlayerInteractionHandler _handler; //Instance of PlayerInteractionHandler
        private GameObject _player; //Instance of player
        private float currTime; //Current time

        private float delay = 5; //Transition delay
        private bool lost; //Lost?

        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player; //Assign player
            _handler = handler; //Assign value
        }

        private void Awake()
        {
            delay = 3f; //Set delay
        }

        private void Update()
        {
            currTime += Time.deltaTime; //Increment time

            if (currTime >= delay)
            {
                if (lost)
                {
                    //Load MainMenu
                    SceneManager.LoadScene("MenuTest");
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

        public void Lose()
        {
            //Destroy player
            PlayerInteractionHandler.SceneObjects.Player.PlayerStates.DestroySelf();
            //Set text color to red
            PlayerInteractionHandler.SceneObjects.UI.Timer.Text.color = Color.red;
            //Stop timer
            PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.StopTimer();
            //Disable camera script
            PlayerInteractionHandler.SceneObjects.Camera.Script.isEnabled = false;

            //Instantiate loss screen
            var text = Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);

            //Get text objects
            var textObjects = text.GetComponentsInChildren<TextMeshProUGUI>();

            //Format text
            textObjects[0].text = "You lost!";
            textObjects[0].color = Color.red;
            textObjects[1].color = Color.red;

            lost = true;

            var go = new GameObject(); //Add empty handler
            go.AddComponent<GameStateManager>(); //Add GameStateManager component to object
        }

        public void Win()
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
            Instantiate(PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab);

            var go = new GameObject(); //Add empty handler
            go.AddComponent<GameStateManager>(); //Add GameStateManager component to object
        }
    }
}