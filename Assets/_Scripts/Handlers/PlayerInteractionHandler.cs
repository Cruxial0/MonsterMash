﻿using System;
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
        private GameObject _player { get; }
        public readonly InteractableHandler InteractableHandler;
        public readonly TrapHandler TrapHandler;
        public readonly GameStateManager GameStateManager;
        public SceneObjects SceneObjects;

        private GameObject _guiParent { get; }
        private TextMeshProUGUI CollectableText { get; set; }
        
        private TextMeshProUGUI TimerText { get; set; }

        private int _collectableCount = 0;
        private int _currCollectable = 0;

        private TimerHandler _timerHandler { get; }
        
        //public FixedJoystick VirtualJoystick { get; set; }

        //Build new instance of class
        public PlayerInteractionHandler(GameObject player)
        {
            CurrentLevel = SceneManager.GetActiveScene();
            _player = player;
            InteractableHandler = new InteractableHandler();
            GameStateManager = new GameStateManager(player, this);
            TrapHandler = new TrapHandler();
            //GameStateManager = new GameStateManager(player, this);
            _guiParent = GameObject.FindGameObjectWithTag("UI");
            SceneObjects = new SceneObjects(CurrentLevel, this);

            CollectableText = _guiParent.transform.GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "CollectableCounter");
            TimerText = _guiParent.transform.GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "Timer");
            //VirtualJoystick = _guiParent.transform.GetComponentsInChildren<FixedJoystick>().First();
            _timerHandler = TimerText.GetComponent<TimerHandler>();

            _timerHandler.TimerDepleted += HandleTimer;
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
            
            Debug.Log(InteractableHandler.Interactibles.Count());
            
            InitializeGUI();
            StartTimer();
            
        }

        private void HandleTrapCollision(object sender, TrapEventArgs e)
        {
            e.ScriptReference.OnCollision(20f);
            SceneObjects.Room.PickupObject.First().SpriteRenderer.color = Color.cyan;
        }

        private void HandleTimer(object sender)
        {
            Debug.Log("timer depleted");
            Object.Destroy(_player);
        }

        public void StartTimer() => _timerHandler.StartTimer();
        public void StopTimer() => _timerHandler.StopTimer();

        private void InitializeGUI()
        {
            _collectableCount = InteractableHandler.Interactibles.Count(x => x.InteractType == InteractType.Pickup);
            Debug.Log(_collectableCount);
            CollectableText.text = $"{_currCollectable}/{_collectableCount}";
        }

        private void UpdateGUI(object sender)
        {
            CollectableText.text = $"{_currCollectable}/{_collectableCount}";
            if (_currCollectable == _collectableCount)
            {
                StopTimer();
                Debug.Log("YOU WIN!!!!");
                //End game
            }
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