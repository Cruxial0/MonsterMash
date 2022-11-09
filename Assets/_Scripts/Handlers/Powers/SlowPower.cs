using System;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Powers
{
    public class SlowPower : UnityEngine.MonoBehaviour, IPower
    {
        private bool active = true;

        //Assign values for timer
        private float currTime;
        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;

        private void Start()
        {
            SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>().sprite =
                SceneObjects.Player.Sprites.Sprites[3];
        }

        private void FixedUpdate()
        {
            //Increment timer
            currTime += Time.deltaTime;
            if (currTime > PowerDuration && active)
            {
                //Revert speed and drag
                SceneObjects.Player.MovmentController.MovementSpeed = 
                    SceneObjects.Player.MovmentController.DefaultMovementSpeed;
                
                SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>().sprite =
                    SceneObjects.Player.Sprites.Sprites[0];
                active = false; //Deactivate timer
            }
        }

        public string PowerName => "SlowPower";
        public float PowerDuration { get; set; }
        public string PowerDescription => "Slows the player down for 3 seconds";
        public PowerObject PowerObject => new(SlowLogic);
        public GameObject Parent { get; set; } = null;

        private void SlowLogic()
        {
            var c = Parent.AddComponent<SlowPower>(); //Add instance of script to object
            c.PowerDuration = this.PowerDuration;

            SceneObjects.Player.MovmentController.MovementSpeed = 10f; //Set speed of player

            //Disable power visuals
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }
    }
}