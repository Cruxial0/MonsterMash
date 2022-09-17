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

            // Add the light component
            Light light = Objects.Player.PlayerLight;
            light.intensity = 2.65f;
            light.range = 15f;
        }
    }
}