using System;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Powers
{
    public class ForcePower : UnityEngine.MonoBehaviour, IPower
    {
        private bool active = true;

        //Assign values for timer
        private readonly float buffTime = 3f;
        private float currTime;

        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;

        private void Start()
        {
            SceneObjects.Player.MovmentController.MovementSpeed = 50f; //Set speed of player
        }

        private void FixedUpdate()
        {
            currTime += Time.deltaTime; //Increment timer
            if (currTime > buffTime && active)
            {
                SceneObjects.Player.MovmentController.MovementSpeed = 20f; //Revert speed of player
                active = false; //Deactivate timer
            }
        }

        public string PowerName => "ForcePower";
        public string PowerDescription => "Adds instant force to the player";

        public PowerObject PowerObject => new(ForceLogic);
        public GameObject Parent { get; set; } = null;

        private void ForceLogic()
        {
            Parent.AddComponent<ForcePower>(); //Add component of power
            
            //Disable power visuals
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }
    }
}