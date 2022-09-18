using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using _Scripts.MonoBehaviour.Interactables.Traps;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class PlayerInteractionHandler
    {
        public Scene CurrentLevel;
        //Reference to player
        public readonly InteractableHandler InteractableHandler;
        public readonly TrapHandler TrapHandler;
        public static GameStateManager GameStateManager;
        public static SceneObjects SceneObjects;

        private int _collectableCount = 0;
        private int _currCollectable = 0;

        //Build new instance of class
        public PlayerInteractionHandler(GameObject player)
        {
            CurrentLevel = SceneManager.GetActiveScene();
            InteractableHandler = new InteractableHandler();
            TrapHandler = new TrapHandler();
            SceneObjects = new SceneObjects(CurrentLevel, this);
            GameStateManager = new GameStateManager(SceneObjects.Player.Self, this);

            SceneObjects.UI.Timer.TimerHandler.TimerDepleted += HandleTimer;
            this.InteractablePickedUp += UpdateGUI;

            foreach (var interactable in InteractableHandler.Interactibles)
            {
                interactable.Parent.GetComponent<InteractableInitialize>().AddInteractionHandlerReference(this);
                interactable.CollisionAdded += HandleCollision;
            }
            
            foreach (var interactable in TrapHandler.Interactibles)
            {
                interactable.Parent.GetComponent<TrapInitialize>().AddInteractionHandlerReference(this);
                interactable.TrapCollisionAdded += HandleTrapCollision;
            }

            InitializeGUI();
            StartTimer();
            
        }

        private void HandleTrapCollision(object sender, TrapEventArgs e)
        {
            e.ScriptReference.OnCollision(20f);
            SceneObjects.Room.TrapObjects.First().SpriteRenderer.color = Color.clear;
        }

        private void HandleTimer(object sender)
        {
            Debug.Log("timer depleted");
            SceneObjects.UI.Timer.Text.color = Color.red;
            Object.Destroy(SceneObjects.Player.Self);
        }

        public void StartTimer() => SceneObjects.UI.Timer.TimerHandler.StartTimer();
        public void StopTimer()  => SceneObjects.UI.Timer.TimerHandler.StopTimer();

        private void InitializeGUI()
        {
            _collectableCount = InteractableHandler.Interactibles.Count(x => x.InteractType == InteractType.Pickup);
            SceneObjects.UI.CollectableCounter.Text.text = $"{_currCollectable}/{_collectableCount}";
        }

        private void UpdateGUI(object sender)
        {
            SceneObjects.UI.CollectableCounter.Text.text = $"{_currCollectable}/{_collectableCount}";
            
            if(SceneObjects.Room.PickupObject.Count == 0)
                SceneObjects.UI.CollectableCounter.Text.color = Color.green;
                SceneObjects.UI.Timer.Text.color = Color.yellow;
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
                    _currCollectable++;
                    var pickupSceneObject = SceneObjects.Room.PickupObject.First(x =>
                        x.Collider == interactableObject.Parent.GetComponent<Collider>());
                    SceneObjects.Room.PickupObject.Remove(pickupSceneObject);

                    OnInteractablePickupEventHandler handler = InteractablePickedUp;
                    handler?.Invoke(this);
                    
                    break;
                
                case InteractType.Trap:
                    triggerEvent.gameObject.GetComponent<TrapInitialize>().OnCollision(20f);
                    break;
            }
        }

        private event OnInteractablePickupEventHandler InteractablePickedUp;

        private delegate void OnInteractablePickupEventHandler(object sender);

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