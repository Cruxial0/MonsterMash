using _Scripts.Interfaces;

namespace _Scripts.Handlers.EventHandlers
{
    public class EventInitialize
    {
        public EventInitialize(ILevel level)
        {
            //If there are no events, return
            if(level == null || level.Events.Count == 0) return;
            //Apply all events to level
            foreach (var levelEvent in level.Events) levelEvent.ApplyEvent();
        }
    }
}