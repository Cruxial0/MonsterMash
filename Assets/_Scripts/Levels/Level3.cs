using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level3 : ILevel
    {
        public int LevelID => 3;
        public Level Level => new Level("Level 3")
        {
            LevelName = "Speedrun lvl 4"
        };
        public float LevelTime => 40f;
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 25.0,
            TwoStarRequirement = 10.0,
            ThreeStarRequirement = 5.0,
            NoiseThreshold = 0.5f
        };
    }
}