using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class BedController : MonoBehaviour
{
    [NonSerialized]
    public bool IsUnderBed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsUnderBed = true;
            PlayerInteractionHandler.SceneObjects.Player.MeshRenderer.enabled = false;

            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count == 0)
            {
                PlayerInteractionHandler.GameStateManager.Win();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;
        PlayerInteractionHandler.SceneObjects.Player.MeshRenderer.enabled = true;
    }
}
