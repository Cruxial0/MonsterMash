using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class NoEventLevel : ILevel
    {
        public int LevelID => 0;

        public Level Level => new Level("NoEventLevel")
        {
            LevelName = "Event-Free Level"
        };

        public List<IEvent> Events => new List<IEvent>() { new NoEvent() };
    }
}