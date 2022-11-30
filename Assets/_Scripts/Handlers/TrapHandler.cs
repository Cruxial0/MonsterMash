using System;
using System.Collections.Generic;
using _Scripts.Handlers.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class TrapHandler
    {
        //Create list of Interactables
        public List<TrapObject> Interactibles = new();

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
        public delegate void TrapCollisionEventAddedEventHandler(object sender, TrapEventArgs e);

        public List<TrapEventArgs> CollisionLog = new(); //Log of collisions

        public ITrapCollision Script; //Script reference

        public TrapObject(GameObject parent, ITrapCollision initialize)
        {
            //Assign values
            Parent = parent;
            Animation = initialize.Animation;
            Script = initialize;
            Evaluate(); //Evaluate properties
        }

        public GameObject Parent { get; } //Parent GameObject
        public Animation Animation { get; set; } //Animation

        //Destroys parent
        public void Destroy()
        {
            Object.Destroy(Parent);
        }

        //Add collision log
        public TrapEventArgs AddCollisionEntry(TrapEventArgs c)
        {
            CollisionLog.Add(c); //Add collision to log
            OnCollisionEnter(c); //Invoke event

            return c;
        }

        //Event for trap collision
        private void OnCollisionEnter(TrapEventArgs c)
        {
            var handler = TrapCollisionAdded;
            handler?.Invoke(this, c);
        }

        public event TrapCollisionEventAddedEventHandler TrapCollisionAdded;

        private void Evaluate()
        {
            if (Parent == null) throw new NullReferenceException();
            if (Animation == null) Animation = new Animation();
        }
    }

    public class TrapEventArgs : EventArgs
    {
        public Collision CollisionEvent;
        public ITrapCollision ScriptReference;
        public Collider TriggerEvent;

        public TrapEventArgs(ITrapCollision scriptReference, Collider trigger = null, Collision collision = null)
        {
            if (trigger == null && collision == null) return;
            TriggerEvent = trigger;
            CollisionEvent = collision;
            ScriptReference = scriptReference;
        }
    }
}