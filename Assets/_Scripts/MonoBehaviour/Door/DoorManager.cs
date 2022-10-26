using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DoorManager : MonoBehaviour
{
    public GameObject DoorPivot;
    public int parentEnterCount;
    public float enterDelay = 3f;
#pragma warning disable CS0108, CS0114
    public bool enabled;
#pragma warning restore CS0108, CS0114
    [NonSerialized] public bool IsInRoom = false;
    
    private float currTime = 0f;
    private float interval;
    private float baseInterval;
    private const float intervalFluctuation = 2f;
    private bool doorOpen = false;
    private float currEnterDelay = 0f;
    private bool parentsApproaching = false;

    private GameObject parentWarning = null;
    private Renderer doorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if(!enabled) Destroy(this);
        var roundTime = PlayerInteractionHandler.SceneObjects.UI.Timer.TimerHandler.roundTime;
        baseInterval = roundTime / parentEnterCount - enterDelay - 5f;
        interval = Random.Range(baseInterval - intervalFluctuation, baseInterval + intervalFluctuation);
        parentWarning = PlayerInteractionHandler.SceneObjects.UI.ParentWarning.Sprite.gameObject;
        doorRenderer = PlayerInteractionHandler.SceneObjects.Room.DoorObject.DoorPivot.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!enabled) return;
        currTime += Time.deltaTime;

        if (currTime >= interval)
        {
            parentsApproaching = true;
            
            if (parentsApproaching)
            {
                currEnterDelay += Time.deltaTime;
                parentWarning.SetActive(!doorRenderer.isVisible);

                if (currEnterDelay >= enterDelay)
                {
                    parentWarning.SetActive(false);
                    currTime = 0f;

                    if (doorOpen)
                    {
                        DoorPivot.transform.Rotate(new Vector3(0,-60, 0));
                        interval = Random.Range(baseInterval - intervalFluctuation - enterDelay, baseInterval);
                        
                        doorOpen = false;
                        IsInRoom = false;
                        parentsApproaching = false;
                        currEnterDelay = 0f;
                        return;
                    }
            
                    interval = 2f;
                    DoorPivot.transform.Rotate(new Vector3(0, 60, 0));
                    doorOpen = true;
                    IsInRoom = true;
                }
            }
        }

        if (!PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.IsUnderBed && IsInRoom)
        {
            //PlayerInteractionHandler.GameStateManager.Lose(LoseCondition.Parents);
        }
    }
}
