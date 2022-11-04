using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level1 : ILevel
    {
        public int LevelID => 1;
        public Level Level => new Level("LVL1")
        {
            LevelName = "Level 1"
        };
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 0.0,
            TwoStarRequirement = 20.0,
            ThreeStarRequirement = 30.0,
            NoiseThreshold = 0.6f
        };
    }
}