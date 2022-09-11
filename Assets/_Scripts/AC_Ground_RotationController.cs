using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace _Scripts
{
    public class AC_Ground_RotationController : MonoBehaviour
    {
        public GameObject player; //Public reference to the player object.
        public float rotationRate; //Rate of which the floor will rotate at.
        public bool useController;
        public bool mainPlayer;
        private const float MaxRotation = 0.3f; //Max rotation angle.
        private const float RegressionRate = 2f; //Used for calculating regress speed
        private Keyboard _keyboard; //Keyboard.

        //Instance for Unity's InputSystem
        private PlayerControls _controls;
        //Callback value for RotateOnPerformed
        private Vector2 _rotate;
        //Controller Sensitivity, who would have guessed
        private const float ControllerSensitivity = 1.3f;

        private void Awake()
        {
            //Instantiate PlayerControls object
            _controls = new PlayerControls();

            //Subscribe to controller events
            //Learn what events are: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
            _controls.Gameplay.Rotate.performed += RotateOnPerformed;
            _controls.Gameplay.Rotate.canceled += ctx => _rotate = Vector2.zero;
        }

        //Function used for event subscription
        private void RotateOnPerformed(InputAction.CallbackContext obj)
        {
            _rotate = obj.ReadValue<Vector2>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            //Set _keyboard variable to current system keyboard.
            _keyboard = Keyboard.current;
        }

        //Called when gameObject becomes active
        private void OnEnable() => _controls.Gameplay.Enable();
        //Called when gameObject becomes inactive
        private void OnDisable() => _controls.Gameplay.Disable();

        // Update is called once per frame
        private void Update()
        {
            //Make variables for all axis for easier access;
            //Using transform.rotation.x etc. is inefficient.
            var rotationX = transform.rotation.x;
            var rotationZ = transform.rotation.z;
            var rotationY = transform.rotation.y;
            
            //if useController is true, proceed
            if (useController)
            {
                //Regress rotation on all axis
                RegressRotation(rotationX, rotationY, rotationZ);
                
                //If the X or Y rotation exceeds MaxRotation, return.
                if (this.transform.rotation.x is > MaxRotation or < -MaxRotation) return;
                if (this.transform.rotation.z is > MaxRotation or < -MaxRotation) return;
                
                //If _rotate = (0,0), return
                if (_rotate == Vector2.zero) return;
                
                //Might need fine tuning
                //Rotate the object with formula: (axis / 9f) * ControllerSensitivity
                this.transform.Rotate(new Vector3(
                        (_rotate.y / 9f) * ControllerSensitivity,
                        0f, 
                        (-_rotate.x / 9f) * ControllerSensitivity), 
                    Space.World);
                
                //Return so no other controls get processed
                return;
            }

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
                    this.transform.Rotate(new Vector3((-rotationRate / RegressionRate) * Time.deltaTime, 0, 0));
                    break;
                //Because X is rotated in the negative direction,
                //reset it by rotating in the positive direction.
                case < 0:
                    this.transform.Rotate(new Vector3((rotationRate / RegressionRate) * Time.deltaTime, 0, 0));
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
                    this.transform.Rotate(new Vector3(0, (-rotationRate / RegressionRate) * Time.deltaTime, 0));
                    break;
                //When rotation on Y axis is less than 0, reset rotation over time.
                case < 0:
                    //Because Y is rotated in the negative direction,
                    //reset it by rotating in the positive direction.
                    this.transform.Rotate(new Vector3(0, (rotationRate / RegressionRate) * Time.deltaTime, 0));
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
                    this.transform.Rotate(new Vector3(0, 0, (rotationRate / RegressionRate) * Time.deltaTime));
                    break;
                //When rotation on Z axis is more than 0, reset rotation over time.
                case > 0:
                    //Because Z is rotated in the positive direction,
                    //reset it by rotating in the negative direction.
                    this.transform.Rotate(new Vector3(0, 0, (-rotationRate / RegressionRate) * Time.deltaTime));
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
                    if (rotationX < 0) rotation.x += rotationRate  * 1.5f;
                    
                    //Rotate floor by the "rotation" variable declared above.
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }

            //Same as W key, but with different axis
            if (_keyboard.sKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(-rotationRate, 0f, 0f);
                    if (rotationX > 0) rotation.x -= rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as W key, but with different axis
            if (_keyboard.aKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, rotationRate);
                    if (rotationZ < 0) rotation.z += rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as W key, but with different axis
            if (_keyboard.dKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, -rotationRate);
                    if (rotationZ > 0) rotation.z -= rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        }

        private Vector3 HandleRotate(Vector3 rotation) 
            => Vector3.Lerp(transform.rotation.eulerAngles, rotation, Time.deltaTime);

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
                    if (rotationX < 0) rotation.x += rotationRate  * 1.5f;
                    
                    //Rotate floor by the "rotation" variable declared above.
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }

            //Same as Up-Arrow key, but with different axis
            if (_keyboard.downArrowKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(-rotationRate, 0f, 0f);
                    if (rotationX > 0) rotation.x -= rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as Up-Arrow key, but with different axis
            if (_keyboard.leftArrowKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, rotationRate);
                    if (rotationZ < 0) rotation.z += rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        
            //Same as Up-Arrow key, but with different axis
            if (_keyboard.rightArrowKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, -rotationRate);
                    if (rotationZ > 0) rotation.z -= rotationRate  * 1.5f;
                    this.transform.Rotate(HandleRotate(rotation)  * Time.deltaTime);
                    return;
                }
            }
        }
    }
}
