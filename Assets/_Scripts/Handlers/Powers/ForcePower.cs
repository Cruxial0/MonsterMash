using System;
using System.Linq;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using cakeslice;
using UnityEngine;

namespace _Scripts.Handlers.Powers
{
    public class ForcePower : UnityEngine.MonoBehaviour, IPower
    {
        private bool active = true;

        //Assign values for timer
        private float buffTime = 2f;
        private float currTime;

        private Color defaultColor;
        private SpriteRenderer spriteRenderer;

        private RuntimeAnimatorController runtimeAnimatorController;

        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;
        
        private void Start()
        {
            SceneObjects.Player.MovmentController.MovementSpeed = 40f; //Set speed of player

            // Get player sprite renderer
            spriteRenderer = SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>();
            defaultColor = spriteRenderer.color;

            // Change color of sprite
            spriteRenderer.color = new Color(255, 124, 124);

            // Temporarily disable the player's animation controller
            runtimeAnimatorController = PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController;
            PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController = null;
        }

        private void FixedUpdate()
        {
            // If player does not exist, destroy this script
            if(SceneObjects.Player == null) Destroy(this);
            
            currTime += Time.deltaTime; //Increment timer
            SceneObjects.Player.PlayerStates.SetBuffed(true);
            //SceneObjects.Player.PlayerStates.OnPlayerBuffed();
            if (currTime > PowerDuration && active)
            {
                SceneObjects.Player.MovmentController.MovementSpeed = 
                    SceneObjects.Player.MovmentController.DefaultMovementSpeed; //Revert speed of player

                // Revert color
                spriteRenderer.color = defaultColor;
                PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController =
                    runtimeAnimatorController;

                active = false; //Deactivate timer
                
                Destroy(this.gameObject); // Destroy this
            }
        }

        private void OnDestroy()
        {
            // Remove the buffed state when this is destroyed
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
            Destroy(Parent.GetComponent<Outline>());
        }
    }
}