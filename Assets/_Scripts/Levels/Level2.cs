using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level2 : ILevel
    {
        public int LevelID => 2;
        public Level Level => new Level("Level 2")
        {
            LevelName = "Speedrun lvl 2"
        };
        public float LevelTime => 50f;
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 10.0,
            TwoStarRequirement = 20.0,
            ThreeStarRequirement = 30.0,
            NoiseThreshold = 0.6f
        };
    }
}