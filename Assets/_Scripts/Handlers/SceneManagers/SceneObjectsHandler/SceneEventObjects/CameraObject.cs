using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneEventObjects
{
    public class CameraViewArea
    {
        public Transform Transform { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Collider Collider { get; set; }
        public SecurityCameraHandler Script { get; set; }
    }
    public class CameraObject : EventObject
    {
        public Transform Transform { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public PlayableDirector PlayableDirector { get; set; }
        public Animator Animator { get; set; }
        public Animation Animation { get; set; }
        public CameraViewArea CameraView { get; set; }
    }
}