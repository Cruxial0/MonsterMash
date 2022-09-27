using System;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Traps
{
    public class IceBucketTrap: UnityEngine.MonoBehaviour, ITrapCollision
    {
        public InteractType Type;
        private PlayerInteractionHandler _handler;

        private Vector3 _playerMovement = new Vector3();
        
        // Start is called before the first frame update
        void Start()
        {
            TrapInstance = this.gameObject;
            Animation = new Animation();
        }

        private void OnTriggerEnter(Collider c)
        {
            if(!c.gameObject.CompareTag("Player")) return;
            _handler.TrapHandler.Interactibles.First(x => x.Script.TrapName == this.TrapName)
                .AddCollisionEntry(new TrapEventArgs(this, trigger: c));
            TrapActive = true;
            _playerMovement = PlayerInteractionHandler.SceneObjects.Player.Rigidbody.velocity;
        }

        private void OnTriggerExit(Collider other)
        {
            TrapActive = false;
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = true;
        }
        
        //Inherited from ITrapCollision
        public string TrapName => "Ice Bucket";
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler) => _handler = handler;

        public void OnCollision(float playerSpeed)
        {
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = false;
        }

        private bool TrapActive = false;
        
        void Update()
        {
            if(TrapActive == false) return;
            PlayerInteractionHandler.SceneObjects.Player.Rigidbody.AddForce((_playerMovement * PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed / 4) * Time.deltaTime);
        }
    }
}