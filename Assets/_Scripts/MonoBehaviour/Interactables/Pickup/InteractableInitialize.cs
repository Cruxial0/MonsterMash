using System.Linq;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Pickup
{
    public class InteractableInitialize : UnityEngine.MonoBehaviour
    {
        public InteractType Type; //Interact type
        public ParticleSystem VisualFeedback; //Visual feedback
        private PlayerInteractionHandler _handler; //Instance of PlayerInteractionHandler
        void Awake()
        {
            //if type is null, gameObject is invalid, thus, destroy.
            if(Type == null) Destroy(this.gameObject);
        }

        //Get reference to PlayerInteractionHandler
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler) => _handler = handler;

        private void OnCollisionEnter(Collision c)
        {
            //If collider is not of tag player, return
            if(!c.collider.CompareTag("Player")) return;
            
            //Find correct object using LINQ
            _handler.InteractableHandler.Interactibles.First(x => x.Parent == this.gameObject)
                .AddCollisionEntry(new CollisionEventArgs(collision: c));
        }
    
        private void OnTriggerEnter(Collider c)
        {
            //If collider is not of tag player, return
            if(!c.gameObject.CompareTag("Player")) return;
            
            //Find correct object using LINQ
            _handler.InteractableHandler.Interactibles.First(x => x.Parent == this.gameObject)
                .AddCollisionEntry(new CollisionEventArgs(trigger: c));
        }
    }
}
