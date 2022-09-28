using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class EmptyLevelTest : ILevel
    {
        public int LevelID => 3;

        public Level Level => new("EmptyLevel")
        {
            LevelName = "Empty Level lmao"
        };

        public List<IEvent> Events => new() { new NoEvent() };
    }
}