using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Powers
{
    public class SlowPower : UnityEngine.MonoBehaviour, IPower
    {
        public string PowerName => "SlowPower";
        public string PowerDescription => "Slows the player down for 3 seconds";
        public PowerObject PowerObject => new PowerObject(SlowLogic);
        public GameObject Parent { get; set; } = null;
        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;
        
        private void SlowLogic()
        {
            Parent.AddComponent<SlowPower>(); //Add instance of script to object
            
            SceneObjects.Player.MovmentController.MovementSpeed = 50f; //Set speed of player
            SceneObjects.Player.Self.gameObject.GetComponent<Rigidbody>().angularDrag = 1f; //Set Angular Drag of player
            
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
            //Increment timer
            currTime += Time.deltaTime;
            if (currTime > buffTime && active)
            {
                //Revert speed and drag
                SceneObjects.Player.MovmentController.MovementSpeed = 70f;
                SceneObjects.Player.Self.gameObject.GetComponent<Rigidbody>().angularDrag = 3f;
                active = false; //Deactivate timer
            }
        }
    }
}