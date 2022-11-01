using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.GUI.NoiseMeter;
using _Scripts.Handlers;
using UnityEngine;

public class SecurityCameraHandler : MonoBehaviour
{
    public float noiseAdd;
    public AudioSource AudioSource;
    private NoiseMeter _noiseMeter;

    private void Start()
    {
        _noiseMeter = PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        OnCameraEnter();
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        
        _noiseMeter.AddNoise(noiseAdd);
        if(AudioSource != null) AudioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        OnCameraExit();
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
