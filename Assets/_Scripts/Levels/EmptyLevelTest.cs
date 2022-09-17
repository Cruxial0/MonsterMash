using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;
using UnityEngine.SceneManagement;

namespace _Scripts.Levels
{
    public class EmptyLevelTest : ILevel
    {
        public int LevelID => 1;

        public Level Level => new Level("EmptyLevel")
        {
            LevelName = "Empty Level lmao"
        };

        public List<IEvent> Events => new List<IEvent>() { new NoEvent() };
    }
}