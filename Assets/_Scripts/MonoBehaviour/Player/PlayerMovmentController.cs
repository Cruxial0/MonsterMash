using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerMovmentController : UnityEngine.MonoBehaviour
    {
        public ControlType ControlPreset; //Control Configuration
        private PlayerControls _controls; //Instance for Unity's InputSystem
        private Vector2 _rotate; //Callback value for RotateOnPerformed
        private Rigidbody _rigidbody; //Attached rigidbody
        private Keyboard _keyboard = Keyboard.current; //Keyboard
        private Gyroscope _gyroscope = Gyroscope.current;
        [NonSerialized]
        public float MovementSpeed = 70f; //MovementSpeed

        private void Awake()
        {
            //Instantiate PlayerControls object
            _controls = new PlayerControls();

            //Subscribe to controller events
            //Learn what events are: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
            _controls.Player.Movement.performed += RotateOnPerformed;
            _controls.Player.Movement.canceled += ctx => _rotate = Vector2.zero;
        }

        private void Start()
        {
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
            InputSystem.EnableDevice(Gyroscope.current);
        }

        //Called when gameObject becomes active
        private void OnEnable() => _controls.Player.Movement.Enable();
        //Called when gameObject becomes inactive
        private void OnDisable() => _controls.Player.Movement.Disable();
    
        private void RotateOnPerformed(InputAction.CallbackContext obj)
        {
            _rotate = obj.ReadValue<Vector2>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (ControlPreset)
            {
                case ControlType.Joystick:
                    MoveJoystick();
                    break;
            
                case ControlType.Keyboard:
                    MoveKeyboard();
                    break;
                
                case ControlType.Gyroscope:
                    MoveGyroscope();
                    break;
            }
        
        }

        private void MoveGyroscope()
        {
            print($"supports gyro: {SystemInfo.supportsGyroscope}");
            if(_gyroscope != null && _gyroscope.enabled)
            {
                print("Gyroscope is enabled");
                print($"{_gyroscope.angularVelocity.x}");
                var velocity = _gyroscope.angularVelocity.ReadValue();
                _rigidbody.AddForce(new Vector3(velocity.x * MovementSpeed, 0, velocity.z * MovementSpeed) * Time.deltaTime, ForceMode.Force);

            }
        }

        private void MoveKeyboard()
        {
        
            if (_keyboard.wKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0, MovementSpeed) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.sKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0,-MovementSpeed) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.aKey.IsPressed())
                _rigidbody.AddForce(new Vector3(-MovementSpeed, 0, 0) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.dKey.IsPressed())
                _rigidbody.AddForce(new Vector3(MovementSpeed, 0, 0) * Time.deltaTime, ForceMode.Force);
        }

        private void MoveJoystick()
        {
            _rigidbody.AddForce(new Vector3(_rotate.x * MovementSpeed, 0, _rotate.y * MovementSpeed) * Time.deltaTime, ForceMode.Force);
        }

        public enum ControlType
        {
            Joystick = 0,
            Keyboard = 1,
            Gyroscope = 2
        }
    }
}
