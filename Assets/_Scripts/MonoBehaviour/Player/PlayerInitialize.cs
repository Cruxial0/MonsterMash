using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.EventHandlers;
using _Scripts.Interfaces;
using UnityEngine.SceneManagement;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerInitialize : UnityEngine.MonoBehaviour
    {
        public static PlayerInteractionHandler PlayerInteractionHandler;
        void Awake()
        {
            //Instantiate PlayerInteractionHandler, the central system.
            PlayerInteractionHandler = new PlayerInteractionHandler(this.gameObject);
            //Initialize the events
            var eventInitialize = new EventInitialize(LevelManager.SelectedLevel);
        }
    }
}
