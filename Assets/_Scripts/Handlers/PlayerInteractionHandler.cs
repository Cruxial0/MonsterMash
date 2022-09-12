using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Handlers
{
    public class PlayerInteractionHandler
    {
        //Reference to player
        private GameObject _player { get; }
        public readonly InteractableHandler InteractableHandler;

        //Build new instance of class
        public PlayerInteractionHandler(GameObject player)
        {
            _player = player;
            InteractableHandler = new InteractableHandler();

            foreach (var interactable in InteractableHandler.Interactibles)
            {
                interactable.Parent.GetComponent<MI_Interactible_Initialize>().AddInteractionHandlerReference(this);
                interactable.CollisionAdded += HandleCollision;
            }
        }
        
        
        private void HandleCollision(object sender, Collision c)
        {
            Debug.Log(c.collider.name);
        }
    }
}