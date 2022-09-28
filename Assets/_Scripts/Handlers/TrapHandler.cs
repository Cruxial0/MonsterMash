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
        //Create list of Interactables
        public List<TrapObject> Interactibles = new List<TrapObject>();

        public TrapHandler()
        {
            //Get all objects with tag 'Trap' in the Scene
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Trap"))
            {
                //Create and add TrapObject to list of Interactables
                Interactibles.Add(new TrapObject(gameObject, gameObject.GetComponent<ITrapCollision>()));
            }
        }
    }
    
    public class TrapObject
    {
        public GameObject Parent { get; } //Parent GameObject
        public Animation Animation { get; set; } //Animation
        public List<TrapEventArgs> CollisionLog = new List<TrapEventArgs>(); //Log of collisions

        public ITrapCollision Script; //Script reference

        public TrapObject(GameObject parent, ITrapCollision initialize)
        {
            //Assign values
            Parent = parent;
            Animation = initialize.Animation;
            Script = initialize;
            Evaluate(); //Evaluate properties
        }

        //Destroys parent
        public void Destroy() => Object.Destroy(Parent);
        
        //Add collision log
        public void AddCollisionEntry(TrapEventArgs c)
        {
            CollisionLog.Add(c); //Add collision to log
            OnCollisionEnter(c); //Invoke event
        }

        //Event for trap collision
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