using System;
using _Scripts.Handlers;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public GameObject WinPrefab;

    [NonSerialized] public bool IsUnderBed;

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;

        //Show player
        //PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsUnderBed = true;

            //Hide player
            //PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.enabled = false;

            //If all objects are picked up, win
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count == 0)
                PlayerInteractionHandler.GameStateManager.Win();
        }
    }
}