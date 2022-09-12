using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
                Debug.Log(gameObject);
            }
        }
    }

    public class InteractableObject
    {
        public GameObject Parent { get; }
        public ParticleSystem VisualFeedback { get; set; }
        public InteractType InteractType { get; }
        public List<Collision> CollisionLog = new List<Collision>();

        public InteractableObject(GameObject parent, MI_Interactible_Initialize initialize)
        {
            Parent = parent;
            VisualFeedback = initialize.VisualFeedback;
            InteractType = initialize.Type;
            Evaluate();
        }

        private void Destroy() => Object.Destroy(Parent);

        public void AddCollisionEntry(Collision c)
        {
            CollisionLog.Add(c);
            OnCollisionEnter(c);
        }

        private void OnCollisionEnter(Collision c)
        {
            PassthroughAddedEventHandler handler = CollisionAdded;
            handler?.Invoke(this, c);
        }
        public event PassthroughAddedEventHandler CollisionAdded;
        public delegate void PassthroughAddedEventHandler(object sender, Collision e);
        
        private void Evaluate()
        {
            if (Parent == null) throw new NullReferenceException();
            if (VisualFeedback == null) VisualFeedback = new ParticleSystem();
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