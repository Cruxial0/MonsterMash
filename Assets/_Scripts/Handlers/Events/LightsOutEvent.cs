using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    public class LightsOutEvent : IEvent
    {
        public string EventName => "Lights Out!";
        public string Description => "Where did everything go?..";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;
        public void ApplyEvent()
        {
            Objects.Room.LightObject.Light.color = Color.black;
            var light = Objects.Player.Self.AddComponent<Light>();
            light.intensity = 1f;
            light.range = 10f;
        }
    }
}