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
        
        
        private void HandleCollision(object sender, CollisionEventArgs c)
        {
            InteractableObject obj = sender as InteractableObject;

            if (c.TriggerEvent == null) HandleCollisionEvent(obj, c.CollisionEvent);
            if(c.CollisionEvent == null) HandleTriggerEvent(obj, c.TriggerEvent);
        }

        private void HandleTriggerEvent(InteractableObject interactableObject, Collider triggerEvent)
        {
            var collisionType = interactableObject.InteractType;

            switch (collisionType)
            {
                case InteractType.Pickup:
                    interactableObject.Destroy();
                    Debug.Log("destroyed in handler");
                    break;
                
                case InteractType.Trap:
                    break;
            }
        }

        private void HandleCollisionEvent(InteractableObject interactableObject, Collision collisionEvent)
        {
            var collisionType = interactableObject.InteractType;

            switch (collisionType)
            {
                case InteractType.Collision:
                    Object.Instantiate(interactableObject.VisualFeedback, collisionEvent.gameObject.transform.position,
                        interactableObject.VisualFeedback.transform.rotation);
                    break;
                
            }
        }

        private void DetermineVisualFeedback(InteractableObject obj, Collision collision)
        {
            //TODO:
            //Play SFX
            //Play animation
            //Properly position origin point of visualFeedback (using Mesh.bounds?)

            Vector3 instantiatePos = obj.Parent.transform.GetChild(0).position;
            var instantiateScale = obj.Parent.transform.localScale;
            instantiatePos.y = 0;

            if (collision.gameObject.transform.position.z < instantiatePos.z)
                instantiatePos.z -= instantiateScale.z / 0.5f;
            else
                instantiatePos.z += instantiateScale.z / 0.5f;

            //Create instance of visualFeedback (probably some kind of animation or particle system)
            //Also create it with it's original rotation, and on our gameObject's position.
            Object.Instantiate(obj.VisualFeedback, instantiatePos, obj.VisualFeedback.transform.rotation);
        }
    }
}