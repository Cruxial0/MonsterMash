using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace _Scripts.Levels
{
    public class Level5 : ILevel
    {
        public int LevelID => 6;
        public Level Level => new Level("LVL6")
        {
            LevelName = "Level 6"
        };
        public List<IEvent> Events => new() { new SecurityCameraEvent() };
        
        public StarLevels StarLevels => new StarLevels()
        {
            OneStarRequirement = 0.0,
            TwoStarRequirement = 0.0,
            ThreeStarRequirement = 0.0
        };
    }
}