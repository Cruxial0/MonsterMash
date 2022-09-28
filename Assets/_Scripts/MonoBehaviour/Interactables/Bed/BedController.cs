using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class BedController : MonoBehaviour
{
    [NonSerialized]
    public bool IsUnderBed = false;

    public GameObject WinPrefab;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsUnderBed = true;
            
            //Hide player
            PlayerInteractionHandler.SceneObjects.Player.MeshRenderer.enabled = false;

            //If all objects are picked up, win
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count == 0)
            {
                PlayerInteractionHandler.GameStateManager.Win();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;
        
        //Show player
        PlayerInteractionHandler.SceneObjects.Player.MeshRenderer.enabled = true;
    }
}
