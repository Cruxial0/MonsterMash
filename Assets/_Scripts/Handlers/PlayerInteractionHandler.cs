using System;
using System.Linq;
using _Scripts.Handlers.Interfaces;
using _Scripts.Handlers.Powers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using _Scripts.MonoBehaviour.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace _Scripts.Handlers
{
    public class PlayerInteractionHandler
    {
        public static GameStateManager GameStateManager; //GameStateManager
        public static SceneObjects SceneObjects; //SceneObjects
        public static readonly PowerManager PowerManager = new(); //PowerManager
        public static PlayerInteractionHandler Self; //this
        public readonly InteractableHandler InteractableHandler; //InteractableHandler
        public readonly TrapHandler TrapHandler; //TrapHandler
        public readonly PlayerMovmentController.ControlType ControlType;

        private int _collectableCount; //Amount of pickups
        private int _currCollectable; //Current amount picked up
        public Scene CurrentLevel; //Active Level

        //Build new instance of class
        public PlayerInteractionHandler(GameObject player, PlayerMovmentController.ControlType controlType = PlayerMovmentController.ControlType.Joystick)
        {
            CurrentLevel = SceneManager.GetActiveScene(); //CurrentLevel = ActiveScene

            //Instantiate objects
            ControlType = controlType;
            InteractableHandler = new InteractableHandler();
            TrapHandler = new TrapHandler();
            SceneObjects = new SceneObjects(CurrentLevel, this);
            GameStateManager = new GameStateManager(SceneObjects.Player.Self, this);

            PowerManager.GetAll(); //Get all powers

            Self = this;

            //Subscribe to events
            SceneObjects.UI.Timer.TimerHandler.TimerDepleted += HandleTimer;
            InteractablePickedUp += UpdateGUI;

            //Get all pickups
            foreach (var interactable in InteractableHandler.Interactibles)
            {
                interactable.Parent.GetComponent<InteractableInitialize>().AddInteractionHandlerReference(this);
                interactable.CollisionAdded += HandleCollision;
            }

            //Get all traps
            foreach (var interactable in TrapHandler.Interactibles)
            {
                interactable.Parent.GetComponent<ITrapCollision>().AddInteractionHandlerReference(this);
                interactable.TrapCollisionAdded += HandleTrapCollision;
            }

            InitializeGUI();
            StartTimer();
        }

        private void HandleTrapCollision(object sender, TrapEventArgs e)
        {
            //Execute script collision script
            e.ScriptReference.OnCollision(20f);
        }

        private void HandleTimer(object sender)
        {
            //Set color to red and destroy player
            SceneObjects.UI.Timer.Text.color = Color.red;
            GameStateManager.Lose(LoseCondition.Time);
            Object.Destroy(SceneObjects.Player.Self);
        }

        //Starts the timer
        public void StartTimer()
        {
            SceneObjects.UI.Timer.TimerHandler.StartTimer();
        }

        //Stops the timer
        public void StopTimer()
        {
            SceneObjects.UI.Timer.TimerHandler.StopTimer();
        }

        private void InitializeGUI()
        {
            //Gets amount of pickups in scene
            _collectableCount = InteractableHandler.Interactibles.Count(x => x.InteractType == InteractType.Pickup);
            //Set text
            SceneObjects.UI.CollectableCounter.Text.text = $"{_currCollectable}/{_collectableCount}";
        }

        private void UpdateGUI(object sender)
        {
            //Set text
            SceneObjects.UI.CollectableCounter.Text.text = $"{_currCollectable}/{_collectableCount}";

            //Change color of text when all pickups are picked up
            if (SceneObjects.Room.PickupObject.Count == 0)
            {
                SceneObjects.UI.CollectableCounter.Text.color = Color.green;
                SceneObjects.UI.Timer.Text.color = Color.yellow;
            }
        }

        private void HandleCollision(object sender, CollisionEventArgs c)
        {
            //Get event sender from event args
            var obj = sender as InteractableObject;

            //Handle Triggers and Collisions separately
            if (c.TriggerEvent == null) HandleCollisionEvent(obj, c.CollisionEvent);
            if (c.CollisionEvent == null) HandleTriggerEvent(obj, c.TriggerEvent);
        }

        private void HandleTriggerEvent(InteractableObject interactableObject, Collider triggerEvent)
        {
            //Get the collision type of object
            var collisionType = interactableObject.InteractType;

            switch (collisionType)
            {
                //In case of type Pickup:
                case InteractType.Pickup:
                    interactableObject.Destroy(); //Destroy object

                    _currCollectable++; //Increment currCollectable

                    //Get SceneObject from LINQ expression
                    var pickupSceneObject = SceneObjects.Room.PickupObject.First(x =>
                        x.Collider == interactableObject.Parent.GetComponent<Collider>());

                    Vector3 itemPos = SceneObjects.Player.Rigidbody.position;
                    var obj = Object.Instantiate(pickupSceneObject.Script.PopupPrefab, new Vector3(itemPos.x, itemPos.y + 0.4f, itemPos.z), pickupSceneObject.Script.PopupPrefab.transform.rotation);
                    
                    //Remove SceneObject from PickupObject list 
                    SceneObjects.Room.PickupObject.Remove(pickupSceneObject);

                    //Invoke event
                    var handler = InteractablePickedUp;
                    handler?.Invoke(this);

                    break;
                //In case of type Trap:
                case InteractType.Trap:
                    //Execute ITrapCollision.OnCollision() method
                    triggerEvent.gameObject.GetComponent<ITrapCollision>().OnCollision(20f);
                    break;
            }
        }

        //Pickup event handlers
        private event OnInteractablePickupEventHandler InteractablePickedUp;

        private void HandleCollisionEvent(InteractableObject interactableObject, Collision collisionEvent)
        {
            //Get type of collision
            var collisionType = interactableObject.InteractType;

            switch (collisionType)
            {
                //In case of type Collision:
                case InteractType.Collision:
                    //Instantiate VisualFeedback
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.ChangeColor(1);
                    break;
            }
        }

        /// <summary>
        ///     Currently obsolete, might use later.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="collision"></param>
        private void DetermineVisualFeedback(InteractableObject obj, Collision collision)
        {
            //TODO:
            //Play SFX
            //Play animation
            //Properly position origin point of visualFeedback (using Mesh.bounds?)

            var instantiatePos = obj.Parent.transform.GetChild(0).position;
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

        private delegate void OnInteractablePickupEventHandler(object sender);
    }
}