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
        void Start()
        {
            //LevelManager.SelectedLevel.Level.LevelScene = this.gameObject.scene;
            PlayerInteractionHandler = new PlayerInteractionHandler(this.gameObject);
            var eventInitialize = new EventInitialize(LevelManager.SelectedLevel);
        }
    }
}
