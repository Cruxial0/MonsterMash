using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler
{
    public class SceneObjects
    {
        private readonly List<GameObject> rootObjets = new List<GameObject>();
        private PlayerInteractionHandler _handler;
        private readonly SceneObjectInterpreter _interpreter = new SceneObjectInterpreter();

        public readonly SceneObjects Defaults;
        public RoomSceneObject Room;
        public CameraSceneObject Camera;
        public PlayerSceneObject Player;
        public UISceneObject UI;

        private void GetReferences()
        {
            Room = _interpreter.GetRoom(rootObjets);
            Player = _interpreter.GetPlayer(rootObjets);
            Camera = _interpreter.GetMainCamera(rootObjets);
            UI = _interpreter.GetGUI(rootObjets);
        }

        public SceneObjects(Scene level, PlayerInteractionHandler handler)
        {
            _handler = handler;
            level.GetRootGameObjects(rootObjets);
            GetReferences();
            
            Defaults = this;
        }
    }
}