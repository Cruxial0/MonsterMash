using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneEventObjects;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    public class SecurityCameraEvent : UnityEngine.MonoBehaviour, IEvent
    {
        public string EventName => "Security Cameras";
        public string Description => "Adds security cameras";
        public SceneObjects Objects => PlayerInteractionHandler.SceneObjects;
        private CameraObject _cameraObject;
        private Color c;
        private static readonly int TintColor = Shader.PropertyToID("_TintColor");

        public void ApplyEvent()
        {
            _cameraObject = Objects.Room.EventObjects.First(x => x.GetType() == typeof(CameraObject)) as CameraObject;
            
            c = _cameraObject.CameraView.MeshRenderer.material.GetColor(TintColor);
            
            print(_cameraObject.CameraView.Script.name);
            _cameraObject.CameraView.Script.PlayerInCamera += ScriptOnPlayerInCamera;
            _cameraObject.CameraView.Script.PlayerExitCamera += ScriptOnPlayerExitCamera;
        }

        private void ScriptOnPlayerExitCamera(object sender)
        {
            _cameraObject.CameraView.MeshRenderer.material.SetColor(TintColor, c);
        }

        private void ScriptOnPlayerInCamera(object sender)
        {
            _cameraObject.CameraView.MeshRenderer.material.SetColor(TintColor, new Color(255, 0, 0, c.a)); 
        }
    }
}