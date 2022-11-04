using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class ActiveTile : MonoBehaviour
{
    public bool isByWall;
    private void OnCollisionStay(Collision collisionInfo)
    {
        if (!collisionInfo.collider.CompareTag("Player")) return;

        PlayerInteractionHandler.SceneObjects.Room.ActiveFloorTile =
            new _Scripts.Handlers.SceneManagers.SceneObjectsHandler.SceneObjectTypes.ActiveTile()
            {
                ActiveFloorBounds = this.GetComponent<Collider>().bounds,
            };
    }
}
