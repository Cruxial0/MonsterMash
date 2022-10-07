using _Scripts.MonoBehaviour.Player;
using UnityEngine;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes
{
    public class PlayerSprite
    {
        public GameObject Plane { get; set; }
        public SpriteMask SpriteMask { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        
    }
    public class PlayerSceneObject
    {
        public GameObject Self { get; set; }
        public Transform Transform { get; set; }
        public Light PlayerLight { get; set; }
        public Collider Collider { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public PlayerSprite Sprite { get; set; }
        public PlayerMovmentController MovmentController { get; set; }
        public PlayerInitialize InitializeScript { get; set; }
        public PlayerStates PlayerStates { get; set; }
        public AssetContainer Sprites { get; set; }
    }
}