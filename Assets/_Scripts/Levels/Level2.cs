using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level2 : ILevel
    {
        public int LevelID => 2;
        public Level Level => new Level("LVL2")
        {
            LevelName = "Speedrun lvl 3"
        };
        public List<IEvent> Events => new() { new NoEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 0.0,
            TwoStarRequirement = 0.0,
            ThreeStarRequirement = 0.0
        };
    }
}