using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;

namespace _Scripts.Interfaces
{
    public interface IEvent
    {
        public string EventName { get; }
        public string Description { get; }
        public SceneObjects Objects { get; }
        public void ApplyEvent();
    }
}