using System;
using System.Collections.Generic;
using _Scripts.Handlers.Interfaces;
using _Scripts.MonoBehaviour.Interactables.Traps;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class TrapHandler
    {
        public List<TrapObject> Interactibles = new List<TrapObject>();

        public TrapHandler()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Trap"))
            {
                Debug.Log(gameObject.GetComponent<ITrapCollision>().TrapName);
                Interactibles.Add(new TrapObject(gameObject, gameObject.GetComponent<ITrapCollision>()));
            }
        }
    }
    
    public class TrapObject
    {
        public GameObject Parent { get; }
        public Animation Animation { get; set; }
        public List<TrapEventArgs> CollisionLog = new List<TrapEventArgs>();

        public ITrapCollision Script;

        public TrapObject(GameObject parent, ITrapCollision initialize)
        {
            Parent = parent;
            Animation = initialize.Animation;
            Script = initialize;
            Evaluate();
        }

        public void Destroy() => Object.Destroy(Parent);

        public void AddCollisionEntry(TrapEventArgs c)
        {
            CollisionLog.Add(c);
            OnCollisionEnter(c);
        }

        private void OnCollisionEnter(TrapEventArgs c)
        {
            TrapCollisionEventAddedEventHandler handler = TrapCollisionAdded;
            handler?.Invoke(this, c);
        }
        
        public event TrapCollisionEventAddedEventHandler TrapCollisionAdded;
        public delegate void TrapCollisionEventAddedEventHandler(object sender, TrapEventArgs e);

        private void Evaluate()
        {
            if (Parent == null) throw new NullReferenceException();
            if (Animation == null) Animation = new Animation();
        }
    }
    
    public class TrapEventArgs : EventArgs
    {
        public Collider TriggerEvent;
        public Collision CollisionEvent;
        public ITrapCollision ScriptReference;
        public TrapEventArgs(ITrapCollision scriptReference, Collider trigger = null, Collision collision = null)
        {
            if (trigger == null && collision == null) return;
            TriggerEvent = trigger;
            CollisionEvent = collision;
            ScriptReference = scriptReference;
        }
    }
}