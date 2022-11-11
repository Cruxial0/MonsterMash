using System;
using _Scripts.Handlers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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

        [NonSerialized]public ControlType ControlPreset; //Control Configuration
        private PlayerControls _controls; //Instance for Unity's InputSystem
        private readonly Gyroscope _gyroscope = Gyroscope.current;
        private readonly Keyboard _keyboard = Keyboard.current; //Keyboard
        private Rigidbody _rigidbody; //Attached rigidbody
        private Vector2 _rotate; //Callback value for RotateOnPerformed
        [NonSerialized] public bool CanControl = true;
        [NonSerialized] public float MovementSpeed = 16f; //MovementSpeed
        [NonSerialized] public readonly float DefaultMovementSpeed = 16f; //MovementSpeed
        [NonSerialized] public FixedJoystick Joystick;
        public bool isFlat = true;
        public Vector3 prevMovement = new Vector3();
        public Vector3 prevVelocity = new Vector3();
        private TextMeshProUGUI text;
        [NonSerialized] public float acceleration = 0;
        private float prevVertical = 0f;
        private float prevHorizontal = 0f;
        private bool initialSpawn = true;
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
            //InputSystem.EnableDevice(Gyroscope.current); //Enable gyro
            Joystick = PlayerInteractionHandler.SceneObjects.UI.MobileJoystick.OnScreenStick;
            //Get the set control preset
            ControlPreset = PlayerInteractionHandler.Self.ControlType;
            
            text = PlayerInteractionHandler.SceneObjects.UI.DebugGUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            this.transform.Rotate(new Vector3(0,90,0));
        }

        // Update is called once per frame
        private void Update()
        {
            if (Joystick == null) Joystick = PlayerInteractionHandler.SceneObjects.UI.MobileJoystick.OnScreenStick;
            //If player cant control, return
            if (!CanControl) return;
            
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

            if (initialSpawn)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                if (Joystick.Direction != Vector2.zero) initialSpawn = false;
                return;
            }
            
            if (Joystick.Direction != Vector2.zero && ControlPreset == ControlType.Joystick)
            {
                transform.eulerAngles = new Vector3( 0, Mathf.Atan2( Joystick.Horizontal, Joystick.Vertical) * 180 / Mathf.PI, 0 );
                prevHorizontal = Joystick.Horizontal;
                prevVertical = Joystick.Vertical;
            }

            if (Joystick.Direction == Vector2.zero && ControlPreset == ControlType.Joystick)
            {
                transform.eulerAngles = new Vector3( 0, Mathf.Atan2( prevHorizontal, prevVertical) * 180 / Mathf.PI, 0 );
            }
                
            if(ControlPreset == ControlType.Gyroscope)
            {
                if(_rigidbody.velocity.x < 0.05f || _rigidbody.velocity.y < 0.05f)
                    return;
                transform.eulerAngles = new Vector3( 0, Mathf.Atan2( _rigidbody.velocity.x, _rigidbody.velocity.y) * 180 / Mathf.PI, 0 );
            }

            var speed = Vector3.Distance(prevMovement, transform.position) / Time.deltaTime;
            var acceleration = speed / Time.deltaTime;
            
            if(acceleration > 1)
                text.text = $"Acceleration: {acceleration}";
            
            prevMovement = transform.position;
            prevVelocity = _rigidbody.angularVelocity;
            this.acceleration = acceleration;

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
        }

        /// <summary>
        ///     This is hilariously broken, so I wont be commenting it yet
        /// </summary>
        private void MoveGyroscope()
        {
            if (!Input.gyro.enabled)
            {
                Input.gyro.enabled = true;
            }
            float moveH = -Input.acceleration.x;
            float moveV = Input.acceleration.y;
            
            print($"{moveH}, {moveV}");
            _rigidbody.AddForce(new Vector3(-moveH, 0, moveV) * MovementSpeed / 2, ForceMode.Force);
        }

        /// <summary>
        ///     Applies force based on WASD controls
        /// </summary>
        private void MoveKeyboard()
        {
            if (_keyboard.wKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0, MovementSpeed * 2) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.sKey.IsPressed())
                _rigidbody.AddForce(new Vector3(0, 0, -MovementSpeed * 2) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.aKey.IsPressed())
                _rigidbody.AddForce(new Vector3(-MovementSpeed * 2, 0, 0) * Time.deltaTime, ForceMode.Force);
            if (_keyboard.dKey.IsPressed())
                _rigidbody.AddForce(new Vector3(MovementSpeed * 2, 0, 0) * Time.deltaTime, ForceMode.Force);
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