using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Handlers.Powers
{
    public class ForcePower : IPower
    {
        public string PowerName => "ForcePower";
        public string PowerDescription => "Adds instant force to the player";

        public PowerObject PowerObject => new PowerObject(ForceLogic);
        public GameObject Parent { get; set; }
        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;

        private void ForceLogic()
        {
            SceneObjects.Player.Rigidbody.AddForce(new Vector3(0, 0, 400f), ForceMode.Force);
        }
    }
}