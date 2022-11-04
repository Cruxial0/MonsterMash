using System;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Camera
{
    public class CameraPositionController : UnityEngine.MonoBehaviour
    {
        public GameObject player; //Public reference to the player object.
        public bool isEnabled; //Public bool (true/false) to determine if this code will be executed.
        public float CameraDistace = 7f;
        public float halfViewport;
        
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
            
            if (_sceneObjects != null && floorBounds != _sceneObjects.Room.ActiveFloorBounds)
                floorBounds = _sceneObjects.Room.ActiveFloorBounds;
        }

        // Update is called once per frame
        private void Update()
        {
            //if isEnabled is false or player is destroyed, return.
            if (!isEnabled || PlayerDestroyed) return; //return will skip all the code underneath.

            if (_sceneObjects != null && floorBounds != _sceneObjects.Room.ActiveFloorBounds)
                floorBounds = _sceneObjects.Room.ActiveFloorBounds;
            
            //Define a variable of type Vector3 for player position.
            var playerPos = new Vector3();

            //player.transform.position.x cannot be accessed directly, so put it in a variable first.
            var position = player.transform.position;

            //Assign position values from player to newly created playerPos variable
            playerPos.x = position.x;
            playerPos.z = position.z - 2f;
            playerPos.y = position.y + CameraDistace; //+CameraDistance for a consistent height above ground.

            //Clip camera to room bounds
            if (playerPos.x >= floorBounds.extents.x + 2 - halfViewport) 
                playerPos.x = floorBounds.extents.x + 2 - halfViewport;
            if (playerPos.x <= -floorBounds.extents.x - 2 + halfViewport) 
                playerPos.x = -floorBounds.extents.x - 2 + halfViewport;
            if (playerPos.z >= floorBounds.extents.z + 1 - (halfViewport / 2)) 
                playerPos.z = floorBounds.extents.z + 1 - (halfViewport / 2);
            if (playerPos.z <= -floorBounds.extents.z - 3 + (halfViewport / 2)) 
                playerPos.z = -floorBounds.extents.z - 3 + (halfViewport / 2);
            
            //Apply position to camera.
            transform.position = playerPos;
            print(PlayerInteractionHandler.SceneObjects.Room.ActiveFloorBounds);
        }
        
        private float WorldBoundaryReached()
        {
            float playerXPosition = player.transform.position.x;
            Debug.Log(playerXPosition);
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