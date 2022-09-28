using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class LightsOutLevel : ILevel
    {
        public int LevelID => 2;

        public Level Level => new("LightsOutLevel")
        {
            LevelName = "LightsOutEvent"
        };

        public List<IEvent> Events => new() { new LightsOutEvent(), new ZoomEvent() };
    }
}