using System;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.MonoBehaviour.Floor;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Camera
{
    public class CameraPositionController : UnityEngine.MonoBehaviour
    {
        // Adjust camera bounds
        private float PosX = 5.5f;
        private float NegX = 5.73f;
        private float PosZ = 0;
        private float NegZ = 4.2f;
        
        public GameObject player; //Public reference to the player object.
        public bool isEnabled; //Public bool (true/false) to determine if this code will be executed.
        public float CameraDistace = 7f;
        private float halfViewport;
        
        [NonSerialized] public bool PlayerDestroyed;

        private UnityEngine.Camera _camera;
        private Bounds floorBounds;
        private SceneObjects _sceneObjects;

        private int i = 0;
        
        private void Start()
        {
            _camera = this.GetComponent<UnityEngine.Camera>();
            halfViewport = _camera.orthographicSize * _camera.aspect;
            
            // floorBounds = PlayerInteractionHandler.SceneObjects.Room.Floor
            //     .First(x => x.GetComponent<Collider>().bounds.Contains(player.transform.position))
            //     .GetComponent<Collider>().bounds;
            //
            
            _sceneObjects = PlayerInteractionHandler.SceneObjects;
            
            
        }

        // Update is called once per frame
        private void Update()
        {
            //if isEnabled is false or player is destroyed, return.
            if (!isEnabled || PlayerDestroyed) return; //return will skip all the code underneath.

            var tile = _sceneObjects.Room.ActiveFloorTile;
            floorBounds = tile.ActiveFloorBounds;
            
            //Define a variable of type Vector3 for player position.
            var playerPos = new Vector3();

            //player.transform.position.x cannot be accessed directly, so put it in a variable first.
            var position = player.transform.position;

            //Assign position values from player to newly created playerPos variable
            playerPos.x = position.x;
            playerPos.z = position.z - 1f;
            playerPos.y = position.y + CameraDistace; //+CameraDistance for a consistent height above ground.

            //print(tile.ConstraintAxis);
            //Clip camera to room bounds
            if (playerPos.x >= floorBounds.center.x + floorBounds.extents.x + PosX - halfViewport
                && tile.ConstraintAxis.HasFlag(ConstraintAxis.Right))
                playerPos.x = floorBounds.center.x + floorBounds.extents.x + PosX - halfViewport;
            if (playerPos.x <= floorBounds.center.x - floorBounds.extents.x - NegX + halfViewport
                && tile.ConstraintAxis.HasFlag(ConstraintAxis.Left))
                playerPos.x = floorBounds.center.x - floorBounds.extents.x - NegX + halfViewport;
            if (playerPos.z >= floorBounds.center.z + floorBounds.extents.z - PosZ - (halfViewport / 2)
                && tile.ConstraintAxis.HasFlag(ConstraintAxis.Up))
                playerPos.z = floorBounds.center.z + floorBounds.extents.z - PosZ - (halfViewport / 2);
            if (playerPos.z <= floorBounds.center.z -floorBounds.extents.z - NegZ + (halfViewport / 2)
                && tile.ConstraintAxis.HasFlag(ConstraintAxis.Down))
                playerPos.z = floorBounds.center.z -floorBounds.extents.z - NegZ + (halfViewport / 2);
            
            //Apply position to camera.
            transform.position = playerPos;
        }
        
        private float WorldBoundaryReached()
        {
            float playerXPosition = player.transform.position.x;
            if (playerXPosition + halfViewport >= floorBounds.extents.x)
            {
                return floorBounds.extents.x - halfViewport;
            }
            else if (playerXPosition - halfViewport <= -floorBounds.extents.x)
            {
                return floorBounds.extents.x + halfViewport;
            }
            else
                return 0f;
        }
    }
}