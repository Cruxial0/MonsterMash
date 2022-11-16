using System;
using System.Collections.Generic;
using _Scripts.Extensions.Materials;
using _Scripts.Handlers;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public GameObject WinPrefab;

    [NonSerialized] public bool GameStarted = false;
    [NonSerialized] public bool IsUnderBed;
    private MeshRenderer _meshFilters = new();

    private void Start()
    {
        _meshFilters = PlayerInteractionHandler.SceneObjects.Room.BedObject.MeshRenderer;
    }

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;
        
        var c = _meshFilters.material.color;
        c.a = 1f;
        _meshFilters.material.color = new Color(c.r, c.g, c.b, c.a);

        //Show player
        //PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.enabled = true;
        GameStarted = true;

        var handler = OnBedExit;
        handler?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsUnderBed = true;

            var c = _meshFilters.material.color;
            c.a = 0.5f;
            _meshFilters.material.color = c;
            

            //If all objects are picked up, win
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count == 0)
                PlayerInteractionHandler.GameStateManager.Win(PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.currTime);
        }
    }

    private void FixedUpdate()
    {
        PlayerInteractionHandler.SceneObjects.Room.BedObject.OuterCollider.enabled = GameStarted;
    }

    public event OnBedExitEventHandler OnBedExit;
    public delegate void OnBedExitEventHandler();
}