using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level6 : ILevel
    {
        public int LevelID => 5;
        public Level Level => new Level("Level 5")
        {
            LevelName = "Percision lvl 5"
        };
        public float LevelTime => 40;
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 5.0,
            TwoStarRequirement = 10.0,
            ThreeStarRequirement = 18.0,
            NoiseThreshold = 0.5f
        };
    }
}