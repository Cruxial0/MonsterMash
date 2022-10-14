using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace DefaultNamespace
{
    public class Level1 : ILevel
    {
        public int LevelID => 1;
        public Level Level => new Level("Level1")
        {
            LevelName = "Level 1"
        };
        public List<IEvent> Events => new() { new NoEvent() };
    }
}