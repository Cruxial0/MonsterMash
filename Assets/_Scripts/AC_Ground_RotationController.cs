using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AC_Ground_RotationController : MonoBehaviour
{
    public GameObject player;
    public float rotationRate;
    private const float _maxRotation = 0.30f;
    private Keyboard _keyboard;
    // Start is called before the first frame update
    void Start()
    {
        _keyboard = Keyboard.current;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_keyboard.wKey.IsPressed())
        {
            if(transform.rotation.x is >= _maxRotation or <= -_maxRotation) return;
            this.transform.Rotate(new Vector3(rotationRate, 0, 0));
            return;
        }

        if (_keyboard.sKey.IsPressed())
        {
            if(transform.rotation.x is >= _maxRotation or <= -_maxRotation) return;
            this.transform.Rotate(new Vector3(-rotationRate, 0, 0));
            return;
        }
        
        if (_keyboard.aKey.IsPressed())
        {
            if(transform.rotation.z is >= _maxRotation or <= -_maxRotation) return;
            this.transform.Rotate(new Vector3(0, 0, rotationRate));
            return;
        }
        
        if (_keyboard.dKey.IsPressed())
        {
            if(transform.rotation.z is >= _maxRotation or <= -_maxRotation) return;
            this.transform.Rotate(new Vector3(0, 0, -rotationRate));
            return;
        }
        
        if(transform.rotation.x == 0 && transform.rotation.z == 0) return;
        
        if(transform.rotation.x > 0) 
            this.transform.Rotate(-rotationRate, 0, 0);
        if(transform.rotation.x < 0) 
            this.transform.Rotate(rotationRate, 0, 0);
        if(transform.rotation.z < 0) 
            this.transform.Rotate(0, 0, rotationRate);
        if(transform.rotation.z > 0) 
            this.transform.Rotate(0, 0, -rotationRate);
        if(transform.rotation.y > 0) 
            this.transform.Rotate(0, -rotationRate, 0);
        if(transform.rotation.y < 0) 
            this.transform.Rotate(0, rotationRate, 0);
    }
}
