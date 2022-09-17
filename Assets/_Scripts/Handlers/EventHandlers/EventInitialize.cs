using _Scripts.Interfaces;

namespace _Scripts.Handlers.EventHandlers
{
    public class EventInitialize
    {
        public EventInitialize(ILevel level)
        {
            foreach (var levelEvent in level.Events)
            {
                levelEvent.ApplyEvent();
            }
        }
    }
}