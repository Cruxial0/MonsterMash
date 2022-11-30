using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level7 : ILevel
    {
        public int LevelID => 7;
        public Level Level => new Level("DarkLevel")
        {
            LevelName = "Dark Level"
        };
        public float LevelTime => 60f;
        public List<IEvent> Events => new() { new LightsOutEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 0.0,
            TwoStarRequirement = 0.0,
            ThreeStarRequirement = 0.0
        };
    }
}