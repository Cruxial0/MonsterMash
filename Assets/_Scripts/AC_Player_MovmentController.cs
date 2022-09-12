using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class AC_Player_MovmentController : MonoBehaviour
{
    //Instance for Unity's InputSystem
    private PlayerControls _controls;
    //Callback value for RotateOnPerformed
    private Vector2 _rotate;
    //Controller Sensitivity, who would have guessed
    private const float ControllerSensitivity = 0.5f;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        //Instantiate PlayerControls object
        _controls = new PlayerControls();

        //Subscribe to controller events
        //Learn what events are: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
        _controls.Gameplay.Rotate.performed += RotateOnPerformed;
        _controls.Gameplay.Rotate.canceled += ctx => _rotate = Vector2.zero;
    }

    private void RotateOnPerformed(InputAction.CallbackContext obj)
    {
        _rotate = obj.ReadValue<Vector2>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
