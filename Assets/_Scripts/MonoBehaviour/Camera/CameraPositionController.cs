using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Camera
{
    public class CameraPositionController : UnityEngine.MonoBehaviour
    {
        public GameObject player; //Public reference to the player object.
        public bool isEnabled; //Public bool (true/false) to determine if this code will be executed.
        [NonSerialized]
        public bool PlayerDestroyed;
    
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            
            //if isEnabled is false, return.
            if(!isEnabled || PlayerDestroyed) return; //return will skip all the code underneath.

            //Define a variable of type Vector3 for player position.
            Vector3 playerPos = new Vector3();
            
            //player.transform.position.x cannot be accessed directly, so put it in a variable first.
            var position = player.transform.position;
            
            //Assign position values from player to newly created playerPos variable
            playerPos.x = position.x;
            playerPos.z = position.z;
            playerPos.y = position.y + 14f; //+14f for a consistent height above ground.

            //Apply position to camera.
            this.transform.position = playerPos;
        }
    }
}
