using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Traps
{
    public class IceBucketTrap : UnityEngine.MonoBehaviour, ITrapCollision
    {
        public InteractType Type; //Interact type
        private PlayerInteractionHandler _handler; //Reference to PlayerInteractionHandler

        private Vector3 _playerMovement; //Reference to player velocity
        private bool TrapActive; //Trap active?

        // Start is called before the first frame update
        private void Start()
        {
            TrapInstance = gameObject; //Set TrapInstance
            Animation = new Animation(); //Create new empty animation
        }

        private void Update()
        {
            //If trap is not active, return
            if (!TrapActive) return;

            if (PlayerInteractionHandler.SceneObjects.Player.Rigidbody.velocity.magnitude < 1.0f)
            {
                PlayerInteractionHandler.SceneObjects.Player.Rigidbody.AddForce(_playerMovement);
            }
            //Add force to player based on the entry velocity
            PlayerInteractionHandler.SceneObjects.Player.Rigidbody
                .AddForce(_playerMovement * (PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider c)
        {
            //if collider is not type of Player, return
            if (!c.gameObject.CompareTag("Player")) return;

            //Get the correct trap instance using a LINQ expression
            _handler.TrapHandler.Interactibles.First(x => x.Script.TrapName == TrapName)
                .AddCollisionEntry(new TrapEventArgs(this, c));

            //Set trap to active
            TrapActive = true;

            //Get velocity from player
            _playerMovement = PlayerInteractionHandler.SceneObjects.Player.Rigidbody.velocity;
        }

        private void OnTriggerExit(Collider other)
        {
            TrapActive = false; //Deactivate trap
            //Let player control again
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = true;
        }

        //Inherited from ITrapCollision
        public string TrapName => "Ice Bucket";
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }
        
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler)
        {
            _handler = handler;
        }

        public void OnCollision(float playerSpeed)
        {
            //Remove player control
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = false;
        }
    }
}