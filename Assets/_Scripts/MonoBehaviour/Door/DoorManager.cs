using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorManager : MonoBehaviour
{
    public GameObject DoorPivot;
    [NonSerialized] public bool IsInRoom = false;
    
    private float currTime = 0f;
    private float interval;
    private float minRand = 3f;
    private float maxRand = 10f;
    private bool doorOpen = false;
    public GameObject toSprite;

    
    // Start is called before the first frame update
    void Start()
    {
        interval = Random.Range(minRand, maxRand);

        toSprite.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime >= interval)
        {
            currTime = 0f;
            
            if (doorOpen)
            {
                DoorPivot.transform.Rotate(new Vector3(0,-32,0));
                interval = Random.Range(minRand, maxRand);
                doorOpen = false;
                IsInRoom = false;
                toSprite.active = false;
                return;
            }
            
            interval = 2f;
            DoorPivot.transform.Rotate(new Vector3(0,32,0));
            doorOpen = true;
            IsInRoom = true;
            toSprite.active = true;
        }

        if (!PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.IsUnderBed && IsInRoom)
        {
            PlayerInteractionHandler.GameStateManager.Lose();
        }
    }
}
