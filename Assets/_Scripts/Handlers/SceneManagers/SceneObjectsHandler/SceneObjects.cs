using System.Collections.Generic;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler
{
    public class SceneObjects
    {
        //Instance of SceneObjectInterpreter
        private readonly SceneObjectInterpreter _interpreter = new();

        public readonly SceneObjects Defaults; //Scene defaults

        //List of root objects
        private readonly List<GameObject> rootObjets = new();
        private PlayerInteractionHandler _handler; //Instance of PlayerInteraction Handler
        public CameraSceneObject Camera; //Scene Camera
        public PlayerSceneObject Player; //Scene Player
        public RoomSceneObject Room; //Scene Room
        public UISceneObject UI; //Scene UI

        public SceneObjects(Scene level, PlayerInteractionHandler handler)
        {
            _handler = handler; //Assign handler
            level.GetRootGameObjects(rootObjets); //Get root objects
            GetReferences(); //Get references

            Defaults = this; //Set defaults
        }

        private void GetReferences()
        {
            //Get references from interpreter
            Room = _interpreter.GetRoom(rootObjets);
            Player = _interpreter.GetPlayer(rootObjets);
            Camera = _interpreter.GetMainCamera(rootObjets);
            UI = _interpreter.GetGUI(rootObjets);
        }
    }
}