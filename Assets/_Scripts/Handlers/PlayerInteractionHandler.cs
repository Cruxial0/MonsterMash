using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Event = Unity.Services.Analytics.Internal.Event;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class PlayerInteractionHandler
    {
        //Reference to player
        private GameObject _player { get; }
        public readonly InteractableHandler InteractableHandler;
        
        private GameObject _guiParent { get; }
        private TextMeshProUGUI CollectableText { get; set; }
        
        private TextMeshProUGUI TimerText { get; set; }

        private int _collectableCount = 0;
        private int _currCollectable = 0;

        private UI_Timer_Handler _timerHandler { get; }

        //Build new instance of class
        public PlayerInteractionHandler(GameObject player)
        {
            _player = player;
            InteractableHandler = new InteractableHandler();
            _guiParent = GameObject.FindGameObjectWithTag("UI");
            
            CollectableText = _guiParent.transform.GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "UI_Collectable_Text");
            TimerText = _guiParent.transform.GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.name == "UI_Timer");
            _timerHandler = TimerText.GetComponent<UI_Timer_Handler>();

            _timerHandler.TimerDepleted += HandleTimer;
            this.InteractablePickedUp += UpdateGUI;

            foreach (var interactable in InteractableHandler.Interactibles)
            {
                interactable.Parent.GetComponent<MI_Interactible_Initialize>().AddInteractionHandlerReference(this);
                interactable.CollisionAdded += HandleCollision;
            }

            InitializeGUI();
            StartTimer();
        }

        private void HandleTimer(object sender)
        {
            Debug.Log("timer depleted");
            Object.Destroy(_player);
        }

        private void StartTimer() => _timerHandler.StartTimer();
        private void StopTimer() => _timerHandler.StopTimer();

        private void InitializeGUI()
        {
            _collectableCount = InteractableHandler.Interactibles.Count(x => x.InteractType == InteractType.Pickup);
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