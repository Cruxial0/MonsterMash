using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level8 : ILevel
    {
        public int LevelID => 8;
        public Level Level => new Level("DarkLevel")
        {
            LevelName = "DarkLevel"
        };
        public float LevelTime => 40f;
        public List<IEvent> Events => new() { new LightsOutEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 2.0,
            TwoStarRequirement = 5.0,
            ThreeStarRequirement = 10.0,
            NoiseThreshold = 0.5f
        };
    }
}