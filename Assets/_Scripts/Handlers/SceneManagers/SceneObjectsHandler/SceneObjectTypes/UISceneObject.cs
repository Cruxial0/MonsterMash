using _Scripts.GUI.NoiseMeter;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes
{
    public class CanvasObject
    {
        public RectTransform RectTransform { get; set; }
        public Canvas Canvas { get; set; }
        public CanvasScaler CanvasScaler { get; set; }
        public GraphicRaycaster GraphicRaycaster { get; set; }
    }

    public class MobileJoystick
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }
        public Image Image { get; set; }
        public FixedJoystick OnScreenStick { get; set; }
    }

    public class CollectableSprite
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }
        public Image Image { get; set; }
    }

    public class CollectableCounter
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }
        public TextMeshProUGUI Text { get; set; }
    }

    public class Timer
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }
        public TextMeshProUGUI Text { get; set; }
        public TimerHandler TimerHandler { get; set; }
    }

    public class NoiseMeterSceneObject
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }
        public Image Image { get; set; } = null;
        public NoiseProperties NoiseProperties { get; set; }
        public NoiseMeter Script { get; set; }
    }

    public class NoiseProperties
    {
        public int MaxNoise { get; set; }
        public int CurrentNoise { get; set; }
    }

    public class ParentWarningObject
    {
        public RectTransform RectTransform { get; set; }
        public CanvasRenderer CanvasRenderer { get; set; }

        public Image Sprite { get; set; }
    }
    
    public class UISceneObject
    {
        public CanvasObject CanvasObject { get; set; }
        public MobileJoystick MobileJoystick { get; set; }
        public CollectableSprite CollectableSprite { get; set; }
        public CollectableCounter CollectableCounter { get; set; }
        public NoiseMeterSceneObject NoiseMeterSceneObject { get; set; }
        public Timer Timer { get; set; }
        public ParentWarningObject ParentWarning { get; set; }
        public GameObject DebugGUI { get; set; }
    }
}