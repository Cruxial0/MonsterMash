using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AC_Player_MovmentController : MonoBehaviour
{
    public ControlType ControlPreset;
    
    //Instance for Unity's InputSystem
    private PlayerControls _controls;
    //Callback value for RotateOnPerformed
    private Vector2 _rotate;
    //Controller Sensitivity, who would have guessed
    private const float ControllerSensitivity = 4f;
    private Rigidbody _rigidbody;
    private Keyboard _keyboard = Keyboard.current;
    private const float MovementSpeed = 70f;
    private void Awake()
    {
        //Instantiate PlayerControls object
        _controls = new PlayerControls();

        //Subscribe to controller events
        //Learn what events are: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
        _controls.Gameplay.Rotate.performed += RotateOnPerformed;
        _controls.Gameplay.Rotate.canceled += ctx => _rotate = Vector2.zero;
    }

    //Called when gameObject becomes active
    private void OnEnable() => _controls.Gameplay.Enable();
    //Called when gameObject becomes inactive
    private void OnDisable() => _controls.Gameplay.Disable();
    
    private void RotateOnPerformed(InputAction.CallbackContext obj)
    {
        _rotate = obj.ReadValue<Vector2>();
        print(_rotate);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
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
        Debug.LogError("Gyroscope controls not implemented!");
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
        _rigidbody.AddForce(new Vector3(_rotate.x * ControllerSensitivity, 0, _rotate.y * ControllerSensitivity), ForceMode.Force);
    }

    public enum ControlType
    {
        Joystick = 0,
        Keyboard = 1,
        Gyroscope = 2
    }
}
