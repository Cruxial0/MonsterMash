using System;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerMovmentController : UnityEngine.MonoBehaviour
    {
        public enum ControlType
        {
            Joystick = 0,
            Keyboard = 1,
            Gyroscope = 2
        }

        public ControlType ControlPreset; //Control Configuration
        private PlayerControls _controls; //Instance for Unity's InputSystem
        private readonly Gyroscope _gyroscope = Gyroscope.current;
        private readonly Keyboard _keyboard = Keyboard.current; //Keyboard
        private Rigidbody _rigidbody; //Attached rigidbody
        private Vector2 _rotate; //Callback value for RotateOnPerformed
        [NonSerialized] public bool CanControl = true;
        [NonSerialized] public float MovementSpeed = 10f; //MovementSpeed
        [NonSerialized] public readonly float DefaultMovementSpeed = 10f; //MovementSpeed
        [NonSerialized] public FixedJoystick Joystick;

        private void Awake()
        {
            MovementSpeed = DefaultMovementSpeed;
            //Instantiate PlayerControls object
            _controls = new PlayerControls();
            //InputSystem.EnableDevice(Gyroscope.current);

            //Subscribe to controller events
            //Learn what events are: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
            _controls.Player.Movement.performed += RotateOnPerformed;
            _controls.Player.Movement.canceled += ctx => _rotate = Vector2.zero;
        }

        private void Start()
        {
            //Get rigidbody component
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            InputSystem.EnableDevice(Gyroscope.current); //Enable gyro
            Joystick = PlayerInteractionHandler.SceneObjects.UI.MobileJoystick.OnScreenStick;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Joystick == null) Joystick = PlayerInteractionHandler.SceneObjects.UI.MobileJoystick.OnScreenStick;
            //If player cant control, return
            if (!CanControl) return;

            print($"({Joystick.Horizontal}, {Joystick.Vertical})");
            
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

        //Called when gameObject becomes active
        private void OnEnable()
        {
            _controls.Player.Movement.Enable();
        }

        //Called when gameObject becomes inactive
        private void OnDisable()
        {
            _controls.Player.Movement.Disable();
        }

        //Read value from joystick
        private void RotateOnPerformed(InputAction.CallbackContext obj)
        {
            _rotate = obj.ReadValue<Vector2>();
            print(_rotate);
        }

        /// <summary>
        ///     This is hilariously broken, so I wont be commenting it yet
        /// </summary>
        private void MoveGyroscope()
        {
            if (_gyroscope != null && _gyroscope.enabled)
            {
                var velocity = _controls.Player.Gyro.ReadValue<Vector3>();

                print("Gyroscope is enabled");
                print($"clamp constant x: {velocity.normalized}");
                print($"x: {velocity.x}");

                _rigidbody.AddForce(
                    new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime,
                    ForceMode.Force);
            }
        }

        /// <summary>
        ///     Applies force based on WASD controls
        /// </summary>
        private void MoveKeyboard()
        {
            if (_keyboard.wKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0, MovementSpeed) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.sKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0, -MovementSpeed) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.aKey.IsPressed())
                _rigidbody.AddForce(new Vector3(-MovementSpeed, 0, 0) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.dKey.IsPressed())
                _rigidbody.AddForce(new Vector3(MovementSpeed, 0, 0) * Time.deltaTime, ForceMode.Force);
        }

        /// <summary>
        ///     Applies force based on the On-Screen Joystick
        /// </summary>
        private void MoveJoystick()
        {
            _rigidbody.velocity += 
                new Vector3(Joystick.Horizontal * MovementSpeed, 0, Joystick.Vertical * MovementSpeed) * Time.deltaTime;
        }
    }
}