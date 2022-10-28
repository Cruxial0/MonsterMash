using System;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using _Scripts.MonoBehaviour.CommonFunctionality;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Traps
{
    public class LaunchTrap: UnityEngine.MonoBehaviour, ITrapCollision
    {
        private PlayerInteractionHandler _handler; //Reference to PlayerInteractionHandler
        
        public string TrapName => "Launch Trap";
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler)
        {
            _handler = handler;
        }

        private Rigidbody playerBody;

        private void OnTriggerEnter(Collider other)
        {
            print("collided");
            playerBody = PlayerInteractionHandler.SceneObjects.Player.Rigidbody;
            var force = playerBody.velocity;
            playerBody.AddForce(-force, ForceMode.Impulse);
        }

        public void OnCollision(float playerSpeed)
        {
            print("collided");
            playerBody = PlayerInteractionHandler.SceneObjects.Player.Rigidbody;
            var force = playerBody.velocity;
            playerBody.AddForce(-force * playerSpeed, ForceMode.Force);
        }

        private void Start()
        {
            TrapInstance = this.gameObject;
            Animation = new Animation();
        }
    }
}