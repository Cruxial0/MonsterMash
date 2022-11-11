using System.Linq;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Pickup
{
    public class InteractableInitialize : UnityEngine.MonoBehaviour
    {
        public InteractType Type; //Interact type
        public ParticleSystem VisualFeedback; //Visual feedback
        public GameObject PopupPrefab;
        private PlayerInteractionHandler _handler; //Instance of PlayerInteractionHandler

        private void Awake()
        {
            //if type is null, gameObject is invalid, thus, destroy.
        }

        private void OnCollisionEnter(Collision c)
        {
            OnCollisionDetected?.Invoke(c);
            //If collider is not of tag player, return
            if (!c.collider.CompareTag("Player")) return;

            print(_handler == null);
            //Find correct object using LINQ
            PlayerInteractionHandler.Self.InteractableHandler.Interactibles.First(x => x.Parent == gameObject)
                .AddCollisionEntry(new CollisionEventArgs(collision: c));
        }

        private void OnTriggerEnter(Collider c)
        {
            //If collider is not of tag player, return
            if (!c.gameObject.CompareTag("Player")) return;

            //Find correct object using LINQ
            _handler.InteractableHandler.Interactibles.First(x => x.Parent == gameObject)
                .AddCollisionEntry(new CollisionEventArgs(c));
        }

        //Get reference to PlayerInteractionHandler
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler)
        {
            _handler = handler;
        }

        public event OnCollisionDetectedEvent OnCollisionDetected;
        public delegate void OnCollisionDetectedEvent(Collision c);

        protected virtual void OnOnCollisionDetected(Collision c)
        {
            OnCollisionDetected?.Invoke(c);
        }
    }
}