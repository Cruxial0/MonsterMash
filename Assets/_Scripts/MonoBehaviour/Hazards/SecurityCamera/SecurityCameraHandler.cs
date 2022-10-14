using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCameraEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnCameraExit();
        }
    }

    private void OnCameraExit()
    {
        var handler = PlayerExitCamera;
        handler?.Invoke(this);
    }

    public event CameraBoundsExit PlayerExitCamera;
    public delegate void CameraBoundsExit(object sender);
    
    private void OnCameraEnter()
    {
        var handler = PlayerInCamera;
        handler?.Invoke(this);
    }

    public event CameraBoundsEntered PlayerInCamera;
    public delegate void CameraBoundsEntered(object sender);
}
