using System;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Handlers.Powers
{
    public class ForcePower : UnityEngine.MonoBehaviour, IPower
    {
        public string PowerName => "ForcePower";
        public string PowerDescription => "Adds instant force to the player";

        public PowerObject PowerObject => new PowerObject(ForceLogic);
        public GameObject Parent { get; set; } = null;

        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;

        private void ForceLogic()
        {
            Parent.AddComponent<ForcePower>(); //Add component of power
            
            SceneObjects.Player.MovmentController.MovementSpeed = 170f; //Set speed of player
            
            //Disable power visuals
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }

        //Assign values for timer
        private float buffTime = 3f;
        private float currTime = 0f;
        private bool active = true;
        
        private void FixedUpdate()
        {
            currTime += Time.deltaTime; //Increment timer
            if (currTime > buffTime && active)
            {
                SceneObjects.Player.MovmentController.MovementSpeed = 70f; //Revert speed of player
                active = false; //Deactivate timer
            }
        }
    }
}