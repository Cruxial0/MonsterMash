using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace _Scripts
{
    public class AC_Ground_RotationController : MonoBehaviour
    {
        public GameObject player; //Public reference to the player object.
        public float rotationRate; //Rate of which the floor will rotate at.
        public bool mainPlayer;
        private const float MaxRotation = 30f; //Max rotation angle.
        private const float RegressionRate = 2f; //Used for calculating regress speed
        private Keyboard _keyboard; //Keyboard.

        // Start is called before the first frame update
        private void Start()
        {
            //Set _keyboard variable to current system keyboard.
            _keyboard = Keyboard.current;
        }

        // Update is called once per frame
        private void Update()
        {
            //Make variables for all axis for easier access;
            //Using transform.rotation.x etc. is inefficient.
            var rotationX = transform.rotation.x;
            var rotationZ = transform.rotation.z;
            var rotationY = transform.rotation.y;

            switch (mainPlayer)
            {
                case true:
                    MoveWASD(rotationX, rotationZ);
                    RegressRotation(rotationX, rotationY, rotationZ);
                    break;
                
                case false:
                    MoveArrowKeys(rotationX, rotationZ);
                    RegressRotation(rotationX, rotationY, rotationZ);
                    break;
            }
        }

        private void RegressRotation(float rotationX, float rotationY, float rotationZ)
        {
            //A switch statement is a collection of if statements. Removes clutter.
            switch (rotationX)
            {
                //When rotation on X axis = 0, return.
                case 0 when rotationZ == 0:
                    return;
                //When rotation on X axis is more than 0, reset rotation over time.
                case > 0:
                    //Because X is rotated in the positive direction,
                    //reset it by rotating in the negative direction.
                    this.transform.Rotate((-rotationRate / RegressionRate)  * Time.deltaTime, 0, 0);
                    break;
                //Because X is rotated in the negative direction,
                //reset it by rotating in the positive direction.
                case < 0:
                    this.transform.Rotate((rotationRate / RegressionRate)  * Time.deltaTime, 0, 0);
                    break;
                
            }

            //Any of the "break;" statements above will put us here and continue the code.
            //The return will skip everything underneath in this case.
            
            //A switch statement is a collection of if statements. Removes clutter.
            switch (rotationY)
            {
                //When rotation on Y axis is more than 0, reset rotation over time.
                case > 0:
                    //Because Y is rotated in the positive direction,
                    //reset it by rotating in the negative direction.
                    this.transform.Rotate(0, (-rotationRate / RegressionRate)  * Time.deltaTime, 0);
                    break;
                //When rotation on Y axis is less than 0, reset rotation over time.
                case < 0:
                    //Because Y is rotated in the negative direction,
                    //reset it by rotating in the positive direction.
                    this.transform.Rotate(0, (rotationRate / RegressionRate)  * Time.deltaTime, 0);
                    break;
            }
            
            //Any of the "break;" statements above will put us here and continue the code.
            //The return will skip everything underneath in this case.
            
            //A switch statement is a collection of if statements. Removes clutter.
            switch (rotationZ)
            {
                //When rotation on Z axis is less than 0, reset rotation over time.
                case < 0:
                    //Because Z is rotated in the negative direction,
                    //reset it by rotating in the positive direction.
                    this.transform.Rotate(0, 0, (rotationRate / RegressionRate)  * Time.deltaTime);
                    break;
                //When rotation on Z axis is more than 0, reset rotation over time.
                case > 0:
                    //Because Z is rotated in the positive direction,
                    //reset it by rotating in the negative direction.
                    this.transform.Rotate(0, 0, (-rotationRate / RegressionRate)  * Time.deltaTime);
                    break;
            }
            
            //Any of the "break;" statements above will put us here and continue the code.
            //The return will skip everything underneath in this case.
        }

        private void MoveWASD(float rotationX, float rotationZ)
        {
            //Check if W was pressed
            if (_keyboard.wKey.IsPressed())
            {
                //If W was pressed, check if rotation is less than maxRotation
                //and rotation is more than -maxRotation (less than 0.30, more than -0.30)
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    //Create a Vector3 variable for rotation values.
                    var rotation = new Vector3(rotationRate, 0f, 0f);
                    
                    //If floor already has been rotated, add extra momentum.
                    if (rotationX < 0) rotation.x += rotationRate;
                    
                    //Rotate floor by the "rotation" variable declared above.
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }

            //Same as W key, but with different axis
            if (_keyboard.sKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(-rotationRate, 0f, 0f);
                    if (rotationX > 0) rotation.x -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as W key, but with different axis
            if (_keyboard.aKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, rotationRate);
                    if (rotationZ < 0) rotation.z += rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as W key, but with different axis
            if (_keyboard.dKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, -rotationRate);
                    if (rotationZ > 0) rotation.z -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        }

        private void MoveArrowKeys(float rotationX, float rotationZ)
        {
            //Check if Up-Arrow was pressed
            if (_keyboard.upArrowKey.IsPressed())
            {
                //If Up-Arrow was pressed, check if rotation is less than maxRotation
                //and rotation is more than -maxRotation (less than 0.30, more than -0.30)
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    //Create a Vector3 variable for rotation values.
                    var rotation = new Vector3(rotationRate, 0f, 0f);
                    
                    //If floor already has been rotated, add extra momentum.
                    if (rotationX < 0) rotation.x += rotationRate;
                    
                    //Rotate floor by the "rotation" variable declared above.
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }

            //Same as Up-Arrow key, but with different axis
            if (_keyboard.downArrowKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(-rotationRate, 0f, 0f);
                    if (rotationX > 0) rotation.x -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as Up-Arrow key, but with different axis
            if (_keyboard.rightArrowKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, rotationRate);
                    if (rotationZ < 0) rotation.z += rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as Up-Arrow key, but with different axis
            if (_keyboard.leftArrowKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, -rotationRate);
                    if (rotationZ > 0) rotation.z -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        }
    }
}
