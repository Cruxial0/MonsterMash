using _Scripts.MonoBehaviour.Camera;
using UnityEngine;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes
{
    public class CameraSceneObject
    {
        public Camera CameraObject = Camera.main;
        public CameraPositionController Script { get; set; }
    }
}