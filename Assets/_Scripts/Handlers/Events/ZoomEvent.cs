using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;

namespace _Scripts.Handlers.Events
{
    public class ZoomEvent : IEvent
    {
        public string EventName => "Zoooooom!";
        public string Description => "Grants the player a limited vision of their surroundings";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;

        public void ApplyEvent()
        {
            //Enables the zoom feature
            Objects.Camera.Script.isEnabled = true;
            Objects.Camera.Script.CameraDistace = 14f; //Set the camera distance

            //Subscribe to OnPlayerDestroyed event
            Objects.Player.PlayerStates.OnPlayerDestroyed += delegate(bool destroyed)
            {
                //Set PlayerDestroyed bool to passed value
                Objects.Camera.Script.PlayerDestroyed = destroyed;
            };
        }
    }
}