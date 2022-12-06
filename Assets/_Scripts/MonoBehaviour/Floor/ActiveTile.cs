using System;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Floor
{
    /// <summary>
    /// Used for assiging the active tile, which is in turn used for camera constraints
    /// </summary>
    public class ActiveTile : UnityEngine.MonoBehaviour
    {
        public ConstraintAxis ConstraintAxis;
        private void OnCollisionStay(Collision collisionInfo)
        {
            if (!collisionInfo.collider.CompareTag("Player")) return;

            PlayerInteractionHandler.SceneObjects.Room.ActiveFloorTile =
                new _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes.ActiveTile()
                {
                    ActiveFloorBounds = this.GetComponent<Collider>().bounds,
                    ConstraintAxis = this.ConstraintAxis
                };
        }
    }
    
    [Flags]
    public enum ConstraintAxis
    {
        None = 1 << 0,
        Up = 1 << 1,
        Down = 1 << 2,
        Right = 1 << 3,
        Left = 1 << 4
    }
}
