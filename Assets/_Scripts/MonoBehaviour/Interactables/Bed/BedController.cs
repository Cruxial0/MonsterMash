using System;
using System.Collections.Generic;
using _Scripts.Extensions.Materials;
using _Scripts.Handlers;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public GameObject WinPrefab;

    [NonSerialized] public bool IsUnderBed;
    private List<MeshRenderer> _meshFilters = new();

    private void Start()
    {
        _meshFilters.Add(GetComponent<MeshRenderer>());
        for (int i = 0; i < transform.childCount; i++)
        {
            _meshFilters.Add(transform.GetChild(i).gameObject.GetComponent<MeshRenderer>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsUnderBed = false;
        
        foreach (var renderer in _meshFilters)
        {
            var c = renderer.material.color;
            c.a = 1f;
            renderer.material.color = new Color(c.r, c.g, c.b, c.a);
        }

        //Show player
        //PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsUnderBed = true;

            foreach (var renderer in _meshFilters)
            {
                var c = renderer.material.color;
                c.a = 0.5f;
                renderer.material.color = c;
            }
            

            //If all objects are picked up, win
            if (PlayerInteractionHandler.SceneObjects.Room.PickupObject.Count == 0)
                PlayerInteractionHandler.GameStateManager.Win();
        }
    }
}