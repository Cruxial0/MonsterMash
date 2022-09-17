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
        private readonly SceneObjects Defaults;
        private readonly SceneObjectInterpreter _interpreter = new SceneObjectInterpreter();

        public RoomSceneObject Room;

        private void GetReferences()
        {
            Debug.Log(rootObjets.Count);
            Room = _interpreter.GetRoom(rootObjets);
            
        }
        
        public SceneObjects(Scene level, PlayerInteractionHandler handler)
        {
            _handler = handler;
            level.GetRootGameObjects(rootObjets);
            Debug.Log("Scene Objects init");
            GetReferences();
            
            Defaults = this;
        }
    }
}