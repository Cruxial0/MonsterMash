using System.Linq;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneEventObjects;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Events
{
    /// <summary>
    /// A neat little camera event. Didn't end up making the final hand-in
    /// </summary>
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
            // Get the camera prefab using some LINQ
            _cameraObject = Objects.Room.EventObjects.First(x => x.GetType() == typeof(CameraObject)) as CameraObject;
            
            // Get current color for the view cone
            c = _cameraObject.CameraView.MeshRenderer.material.GetColor(TintColor);
            
            // Subscribe to events
            _cameraObject.CameraView.Script.PlayerInCamera += ScriptOnPlayerInCamera;
            _cameraObject.CameraView.Script.PlayerExitCamera += ScriptOnPlayerExitCamera;
        }

        private void ScriptOnPlayerExitCamera(object sender)
        {
            // Resets Color
            _cameraObject.CameraView.MeshRenderer.material.SetColor(TintColor, c);
        }

        private void ScriptOnPlayerInCamera(object sender)
        {
            // Set cone color to a red-ish color
            _cameraObject.CameraView.MeshRenderer.material.SetColor(TintColor, new Color(255, 0, 0, c.a)); 
        }
    }
}