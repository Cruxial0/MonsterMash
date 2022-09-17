using _Scripts.Handlers;
using _Scripts.Interfaces;
using UnityEngine.SceneManagement;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace _Scripts.Levels
{
    public class TestScene : ILevel
    {
        public int LevelID => 0;

        public Level Level => new Level("RoomPrototype", new Scene())
        {
            LevelName = "Test Name"
        };
    }
}