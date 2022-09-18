using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;

namespace _Scripts.Handlers.Events
{
    /// <summary>
    /// Empty Event. Use if no event is present.
    /// </summary>
    public class NoEvent : IEvent
    {
        public string EventName => "No event";
        public string Description => "No event has been implemented";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;
        public void ApplyEvent() { }
    }
}