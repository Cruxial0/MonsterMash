using System;
using System.Collections.Generic;
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
                Interactibles.Add(new TrapObject(gameObject, gameObject.GetComponent<MI_Trap_Initialize>()));
            }
        }
    }
    
    public class TrapObject
    {
        public GameObject Parent { get; }
        public Animation Animation { get; set; }
        public InteractType InteractType { get; }
        public List<TrapEventArgs> CollisionLog = new List<TrapEventArgs>();

        private MI_Trap_Initialize _initialize;

        public TrapObject(GameObject parent, MI_Trap_Initialize initialize)
        {
            Parent = parent;
            Animation = initialize.Animation;
            InteractType = initialize.Type;
            _initialize = initialize;
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
        public MI_Trap_Initialize ScriptReference;
        public TrapEventArgs(MI_Trap_Initialize scriptReference, Collider trigger = null, Collision collision = null)
        {
            if (trigger == null && collision == null) return;
            TriggerEvent = trigger;
            CollisionEvent = collision;
            ScriptReference = scriptReference;
        }
    }
}