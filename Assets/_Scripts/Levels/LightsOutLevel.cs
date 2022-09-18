using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class LightsOutLevel : ILevel
    {
        public int LevelID => 2;

        public Level Level => new Level("LightsOutLevel")
        {
            LevelName = "LightsOutEvent"
        };

        public List<IEvent> Events => new List<IEvent>() { new LightsOutEvent(), new ZoomEvent() };
    }
}