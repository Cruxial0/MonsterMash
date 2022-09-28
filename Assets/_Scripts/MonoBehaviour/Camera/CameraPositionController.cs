using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Camera
{
    public class CameraPositionController : UnityEngine.MonoBehaviour
    {
        public GameObject player; //Public reference to the player object.
        public bool isEnabled; //Public bool (true/false) to determine if this code will be executed.

        [NonSerialized] public float CameraDistace = 7f;

        [NonSerialized] public bool PlayerDestroyed;

        // Update is called once per frame
        private void Update()
        {
            //if isEnabled is false or player is destroyed, return.
            if (!isEnabled || PlayerDestroyed) return; //return will skip all the code underneath.

            //Define a variable of type Vector3 for player position.
            var playerPos = new Vector3();

            //player.transform.position.x cannot be accessed directly, so put it in a variable first.
            var position = player.transform.position;

            //Assign position values from player to newly created playerPos variable
            playerPos.x = position.x;
            playerPos.z = position.z;
            playerPos.y = position.y + CameraDistace; //+CameraDistance for a consistent height above ground.

            //Apply position to camera.
            transform.position = playerPos;
        }
    }
}