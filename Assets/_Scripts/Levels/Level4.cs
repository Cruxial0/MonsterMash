using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;

namespace DefaultNamespace
{
    public class Level4 : ILevel
    {
        public int LevelID => 4;
        public Level Level => new Level("Level4")
        {
            LevelName = "Level4"
        };
        public List<IEvent> Events => new() { new SecurityCameraEvent() };
    }
}