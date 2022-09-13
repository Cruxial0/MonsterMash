using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Event = Unity.Services.Analytics.Internal.Event;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class InteractableHandler
    {
        public List<InteractableObject> Interactibles = new List<InteractableObject>();

        public InteractableHandler()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
            {
                Interactibles.Add(new InteractableObject(gameObject, gameObject.GetComponent<MI_Interactible_Initialize>()));
            }
        }
    }

    public class InteractableObject
    {
        public GameObject Parent { get; }
        public ParticleSystem VisualFeedback { get; set; }
        public InteractType InteractType { get; }
        public List<CollisionEventArgs> CollisionLog = new List<CollisionEventArgs>();

        public InteractableObject(GameObject parent, MI_Interactible_Initialize initialize)
        {
            Parent = parent;
            VisualFeedback = initialize.VisualFeedback;
            InteractType = initialize.Type;
            Evaluate();
        }

        public void Destroy() => Object.Destroy(Parent);

        public void AddCollisionEntry(CollisionEventArgs c)
        {
            CollisionLog.Add(c);
            OnCollisionEnter(c);
        }

        private void OnCollisionEnter(CollisionEventArgs c)
        {
            CollisionEventAddedEventHandler handler = CollisionAdded;
            handler?.Invoke(this, c);
        }
        
        public event CollisionEventAddedEventHandler CollisionAdded;
        public delegate void CollisionEventAddedEventHandler(object sender, CollisionEventArgs e);

        private void Evaluate()
        {
            if (Parent == null) throw new NullReferenceException();
            if (VisualFeedback == null) VisualFeedback = new ParticleSystem();
        }
    }
    
    public class CollisionEventArgs : EventArgs
    {
        public Collider TriggerEvent;
        public Collision CollisionEvent;
        public CollisionEventArgs(Collider trigger = null, Collision collision = null)
        {
            if (trigger == null && collision == null) return;
            TriggerEvent = trigger;
            CollisionEvent = collision;
        }
    }

    [Flags]
    public enum InteractType
    {
        None = 0,
        Pickup = 1 << 1,
        Trap = 1 << 2,
        Collision = 1 << 3
    }
}