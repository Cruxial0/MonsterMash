using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace DefaultNamespace
{
    public class Level2 : ILevel
    {
        public int LevelID => 2;
        public Level Level => new Level("Level2")
        {
            LevelName = "Level 2"
        };
        public List<IEvent> Events => new() { new NoEvent() };
    }
}