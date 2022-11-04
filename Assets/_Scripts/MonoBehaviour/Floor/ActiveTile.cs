using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class ActiveTile : MonoBehaviour
{
    private void OnCollisionStay(Collision collisionInfo)
    {
        if(!collisionInfo.collider.CompareTag("Player")) return;

        PlayerInteractionHandler.SceneObjects.Room.ActiveFloorBounds = this.GetComponent<Collider>().bounds;
    }
}
