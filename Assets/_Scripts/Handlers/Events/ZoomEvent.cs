using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    public class ZoomEvent : IEvent
    {
        public string EventName => "Zoooooom!";
        public string Description => "Grants the player a limited vision of their surroundings";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;
        public void ApplyEvent()
        {
            Objects.Camera.Script.isEnabled = true;
            Objects.Camera.Script.CameraDistace = 14f;
            Debug.Log(Objects.Player.PlayerStates);
            Objects.Player.PlayerStates.OnPlayerDestroyed += delegate(bool destroyed)
            {
                Debug.Log("destroyed");
                Objects.Camera.Script.PlayerDestroyed = destroyed;
                
            };
        }
    }
}