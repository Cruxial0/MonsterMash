using System.Collections.Generic;
using _Scripts.Handlers.Interfaces;
using _Scripts.MonoBehaviour.Interactables.Pickup;
using UnityEngine;

namespace _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes
{
    public class LightSceneObject
    {
        public Transform Transform { get; set; }
        public Light Light { get; set; }
    }

    public class PickupSceneObject
    {
        public Transform Transform { get; set; }
        public Collider Collider { get; set; }
        public SpriteMask SpriteMask { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        public InteractableInitialize Script { get; set; }
    }

    public class FurnitureSceneObject
    {
        public Transform Transform { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public FurnitureBody Body { get; set; }
        public InteractableInitialize Script { get; set; }
    }

    public class FurnitureBody
    {
        public Transform Transform { get; set; }
        public Collider Collider { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
    }

    public class TrapSceneObject
    {
        public Transform Transform { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        public Collider Collider { get; set; }
        public ITrapCollision Script { get; set; }
    }

    public class BedSceneObject
    {
        public Transform Transform { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
        public Rigidbody Rigidbody { get; set; }
        public Collider Collider { get; set; }
        public BedController Script { get; set; }
    }

    public class RoomSceneObject
    {
        public List<FurnitureSceneObject> FurnitureObjects = new();
        public List<PickupSceneObject> PickupObject = new();
        public List<TrapSceneObject> TrapObjects = new();
        public List<GameObject> Walls = new();
        public GameObject ParentObject { get; set; }
        public GameObject Floor { get; set; }
        public List<LightSceneObject> LightObject { get; set; }
        public BedSceneObject BedObject { get; set; }
    }
}