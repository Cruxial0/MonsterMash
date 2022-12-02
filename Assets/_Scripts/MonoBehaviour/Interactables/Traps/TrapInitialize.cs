using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Traps
{
    public class TrapInitialize : UnityEngine.MonoBehaviour, ITrapCollision
    {
        public InteractType Type;
        private PlayerInteractionHandler _handler;

        // Start is called before the first frame update
        private void Start()
        {
            //Create instances
            TrapInstance = gameObject;
            Animation = new Animation();
        }

        private void OnTriggerEnter(Collider c)
        {
            //If collider is not of tag player, return
            if (!c.gameObject.CompareTag("Player")) return;

            //Get the correct trap object using LINQ
            var args = _handler.TrapHandler.Interactibles.First(x => x.Script.TrapName == TrapName)
                .AddCollisionEntry(new TrapEventArgs(this, c));
        }

        //Get reference to PlayerInteractionHandler
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler)
        {
            _handler = handler;
        }

        //Inherited from ITrapCollision
        public string TrapName => "Bear Trap";
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }

        public void OnCollision(float playerSpeed)
        {
            //Lose
            PlayerInteractionHandler.GameStateManager.Lose(LoseCondition.Trap);
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = false;
            PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.GameStarted = true;
            Destroy(this.gameObject);
        }
    }
}