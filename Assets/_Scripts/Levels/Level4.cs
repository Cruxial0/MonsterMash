using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level4 : ILevel
    {
        public int LevelID => 4;
        public Level Level => new Level("Level 4")
        {
            LevelName = "Percision lvl 1"
        };
        public float LevelTime => 60f;
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 0.0,
            TwoStarRequirement = 0.0,
            ThreeStarRequirement = 0.0
        };
    }
}