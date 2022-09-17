using System.Collections.Generic;
using _Scripts.Handlers;
using _Scripts.Handlers.Events;
using _Scripts.Interfaces;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace _Scripts.Levels
{
    public class TestScene : ILevel
    {
        public int LevelID => 0;

        public Level Level => new Level("RoomPrototype")
        {
            LevelName = "Test Name"
        };

        public List<IEvent> Events => new List<IEvent>() { new ZoomEvent() };
    }
}