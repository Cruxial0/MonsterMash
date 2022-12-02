using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level7 : ILevel
    {
        public int LevelID => 7;
        public Level Level => new Level("Level 7")
        {
            LevelName = "Level 7"
        };
        public float LevelTime => 30f;
        public List<IEvent> Events => new() { new LightsOutEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 2.0,
            TwoStarRequirement = 5.0,
            ThreeStarRequirement = 10.0,
            NoiseThreshold = 0.4f
        };
    }
}