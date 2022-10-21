using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace DefaultNamespace
{
    public class Level3 : ILevel
    {
        public int LevelID => 3;
        public Level Level => new Level("Level3")
        {
            LevelName = "Level 3"
        };
        public List<IEvent> Events => new() { new NoEvent() };
    }
}