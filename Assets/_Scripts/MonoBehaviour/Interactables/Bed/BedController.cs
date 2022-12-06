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

    private float confettiTimeout = 2f;
    private float currConfettiTime;
    private GameObject confetti;

    private void Start()
    {
        _meshFilters = PlayerInteractionHandler.SceneObjects.Room.BedObject.MeshRenderer;
        confetti = this.transform.GetChild(0).gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;
        
        // Change color
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
            {
                if(!confetti.activeSelf)
                    confetti.SetActive(true);

                // Disable player control
                PlayerInteractionHandler.SceneObjects.Player.MovmentController.CanControl = false;

                // Timer
                currConfettiTime += Time.deltaTime;
                if(currConfettiTime < confettiTimeout) return;
                
                // Win
                PlayerInteractionHandler.GameStateManager.Win(PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.currTime);

                // Disable all colliders (obsolete becase of new, improved bed model)
                foreach (var collider in this.GetComponents<Collider>())
                {
                    collider.enabled = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Enable or disable access to the bed based on the GameStarted property
        PlayerInteractionHandler.SceneObjects.Room.BedObject.OuterCollider.enabled = GameStarted;
    }

    public event OnBedExitEventHandler OnBedExit;
    public delegate void OnBedExitEventHandler();
}