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
        //List of root objects
        private readonly List<GameObject> rootObjets = new List<GameObject>();
        private PlayerInteractionHandler _handler; //Instance of PlayerInteraction Handler
        //Instance of SceneObjectInterpreter
        private readonly SceneObjectInterpreter _interpreter = new SceneObjectInterpreter();

        public readonly SceneObjects Defaults; //Scene defaults
        public RoomSceneObject Room; //Scene Room
        public CameraSceneObject Camera; //Scene Camera
        public PlayerSceneObject Player; //Scene Player
        public UISceneObject UI; //Scene UI

        private void GetReferences()
        {
            //Get references from interpreter
            Room = _interpreter.GetRoom(rootObjets);
            Player = _interpreter.GetPlayer(rootObjets);
            Camera = _interpreter.GetMainCamera(rootObjets);
            UI = _interpreter.GetGUI(rootObjets);
        }

        public SceneObjects(Scene level, PlayerInteractionHandler handler)
        {
            _handler = handler; //Assign handler
            level.GetRootGameObjects(rootObjets); //Get root objects
            GetReferences(); //Get references
            
            Defaults = this; //Set defaults
        }
    }
}