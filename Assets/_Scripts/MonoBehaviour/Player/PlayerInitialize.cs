using _Scripts.Handlers;
using _Scripts.Handlers.EventHandlers;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerInitialize : UnityEngine.MonoBehaviour
    {
        public static PlayerInteractionHandler PlayerInteractionHandler;

        private void Awake()
        {
            //Instantiate PlayerInteractionHandler, the central system.
            PlayerInteractionHandler = new PlayerInteractionHandler(gameObject);
            //Initialize the events
            var eventInitialize = new EventInitialize(LevelManager.SelectedLevel);
        }
    }
}