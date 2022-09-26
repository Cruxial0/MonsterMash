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
            Parent.AddComponent<SlowPower>();
            print("power activated");
            SceneObjects.Player.MovmentController.MovementSpeed = 50f;
            SceneObjects.Player.Self.gameObject.GetComponent<Rigidbody>().angularDrag = 1f;
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }
        
        private float buffTime = 3f;
        private float currTime = 0f;
        private bool active = true;
        
        private void FixedUpdate()
        {
            currTime += Time.deltaTime;
            if (currTime > buffTime && active)
            {
                print("power deactivated");
                SceneObjects.Player.MovmentController.MovementSpeed = 70f;
                SceneObjects.Player.Self.gameObject.GetComponent<Rigidbody>().angularDrag = 3f;
                active = false;
            }
        }
    }
}