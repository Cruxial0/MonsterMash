using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    public class FloorLavaEvent : IEvent
    {
        public string EventName => "Floor is lava";
        public string Description => "u touch floor u die";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;
        public void ApplyEvent()
        {
            Objects.Room.Floor.gameObject.GetComponent<Material>().color = Color.red;
        }
    }
}