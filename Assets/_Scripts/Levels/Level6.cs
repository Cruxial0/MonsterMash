using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level5 : ILevel
    {
        public int LevelID => 6;
        public Level Level => new Level("Level 6")
        {
            LevelName = "Percision lvl 6"
        };
        public float LevelTime => 45f;
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 5,
            TwoStarRequirement = 10.0,
            ThreeStarRequirement = 15.0,
            NoiseThreshold = 0.5f
        };
    }
}