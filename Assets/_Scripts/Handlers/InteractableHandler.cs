using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class InteractableHandler
    {
        //List of interactables
        public List<InteractableObject> Interactibles = new List<InteractableObject>();

        public InteractableHandler()
        {
            //Get all interactables
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Interactable"))
            {
                Interactibles.Add(new InteractableObject(gameObject, gameObject.GetComponent<InteractableInitialize>()));
            }
        }
    }

    public class InteractableObject
    {
        public GameObject Parent { get; } //Parent
        [CanBeNull] public ParticleSystem VisualFeedback { get; set; } //Visual Feedback
        [CanBeNull] public Animation Animation { get; set; } //Animation
        public InteractType InteractType { get; } //Interactable Type
        public List<CollisionEventArgs> CollisionLog = new List<CollisionEventArgs>(); //Log of collisions

        public InteractableObject(GameObject parent, InteractableInitialize initialize)
        {
            //Assign values
            Parent = parent;
            if(initialize.VisualFeedback != null) 
                VisualFeedback = initialize.VisualFeedback;
            InteractType = initialize.Type;
            Evaluate(); //Evaluate properties
        }

        //Destroy parent
        public void Destroy() => Object.Destroy(Parent);

        //Add Collision Entry
        public void AddCollisionEntry(CollisionEventArgs c)
        {
            CollisionLog.Add(c); //Add collision to log
            OnCollisionEnter(c); //Invoke event
        }

        //Event for Collision
        private void OnCollisionEnter(CollisionEventArgs c)
        {
            CollisionEventAddedEventHandler handler = CollisionAdded;
            handler?.Invoke(this, c);
        }
        
        public event CollisionEventAddedEventHandler CollisionAdded;
        public delegate void CollisionEventAddedEventHandler(object sender, CollisionEventArgs e);

        private void Evaluate()
        {
            //Throw an error if Parent is null
            if (Parent == null) throw new NullReferenceException();
            //If VisualFeedback is null, create new empty ParticleSystem
            if (VisualFeedback == null) VisualFeedback = new ParticleSystem();
        }
    }
    
    /// <summary>
    /// Collision Arguments
    /// </summary>
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