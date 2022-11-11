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
        private float buffTime = 2f;
        private float currTime;

        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;
        
        private static readonly int Emission = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            SceneObjects.Player.MovmentController.MovementSpeed = 30f; //Set speed of player
            
            SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>().sprite =
                SceneObjects.Player.Sprites.Sprites[2];
        }

        private void FixedUpdate()
        {
            if(SceneObjects.Player == null) Destroy(this);
            
            currTime += Time.deltaTime; //Increment timer
            SceneObjects.Player.PlayerStates.SetBuffed(true);
            //SceneObjects.Player.PlayerStates.OnPlayerBuffed();
            if (currTime > PowerDuration && active)
            {
                SceneObjects.Player.MovmentController.MovementSpeed = 
                    SceneObjects.Player.MovmentController.DefaultMovementSpeed; //Revert speed of player
                
                SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>().sprite =
                    SceneObjects.Player.Sprites.Sprites[0];

                active = false; //Deactivate timer
                Destroy(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            SceneObjects.Player.PlayerStates.SetBuffed(false);
        }

        public string PowerName => "ForcePower";
        public float PowerDuration { get; set; }
        public string PowerDescription => "Adds instant force to the player";

        public PowerObject PowerObject => new(ForceLogic);
        public GameObject Parent { get; set; } = null;

        private void ForceLogic()
        {
            var c = Parent.AddComponent<ForcePower>(); //Add component of power
            c.PowerDuration = this.PowerDuration;
            
            //Disable power visuals
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }
    }
}