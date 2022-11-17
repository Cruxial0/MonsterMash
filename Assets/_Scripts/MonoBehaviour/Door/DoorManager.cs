using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DoorManager : MonoBehaviour
{
    public delegate void ParentsEnterEvent(object sender);
    public delegate void ParentsApproachingEvent(object sender);
    
    public GameObject DoorPivot;
    public int parentEnterCount;
    public float enterDelay = 3f;
    public bool enabled;
    private bool isInRoom = false;
    
    private float currTime = 0f;
    private float interval;
    private float baseInterval;
    private const float intervalFluctuation = 2f;
    private bool doorOpen = false;
    private float currEnterDelay = 0f;
    private bool parentsApproaching = false;

    public Boolean ParentsApproaching
    {
        get { return parentsApproaching;}
        set
        {
            if (parentsApproaching != value && parentsApproaching == false)
                OnOnParentsApproach();

            if (parentsApproaching != value && parentsApproaching == true)
                OnParentsLeft();
            
            parentsApproaching = value;
        }
    }

    public Boolean ParentsInRoom
    {
        get { return isInRoom; }
        set
        {
            if (isInRoom != value && isInRoom == false)
                OnParentsEntered();

            isInRoom = value;
        }
    }

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

        ParentsApproaching = false;
        ParentsInRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInteractionHandler.SceneObjects.Player.PlayerStates.Destroyed)
            parentWarning.SetActive(false);
        if(!enabled) return;
        currTime += Time.deltaTime;

        if (currTime >= interval)
        {
            ParentsApproaching = true;
            
            if (ParentsApproaching)
            {
                currEnterDelay += Time.deltaTime;
                //parentWarning.SetActive(!doorRenderer.isVisible);
                parentWarning.SetActive(true);

                if (currEnterDelay >= enterDelay)
                {
                    currTime = 0f;

                    if (doorOpen)
                    {
                        parentWarning.SetActive(false);
                        DoorPivot.transform.Rotate(new Vector3(0,-60, 0));
                        interval = Random.Range(baseInterval - intervalFluctuation - enterDelay, baseInterval);
                        
                        doorOpen = false;
                        ParentsInRoom = false;
                        ParentsApproaching = false;
                        currEnterDelay = 0f;
                        return;
                    }
            
                    interval = 2f;
                    DoorPivot.transform.Rotate(new Vector3(0, 60, 0));
                    doorOpen = true;
                    ParentsInRoom = true;
                }
            }
        }

        if (!PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.IsUnderBed && ParentsInRoom)
        {
            //PlayerInteractionHandler.GameStateManager.Lose(LoseCondition.Parents);
        }
    }

    public delegate void ParentsLeaveEvent(object sender);
    public event ParentsLeaveEvent OnParentsLeave;
    protected virtual void OnParentsLeft() =>  OnParentsLeave?.Invoke(this);
    
    public event ParentsEnterEvent OnParentsEnter;
    protected virtual void OnParentsEntered() => OnParentsEnter?.Invoke(this);

    public event ParentsApproachingEvent OnParentsApproach;
    protected virtual void OnOnParentsApproach() => OnParentsApproach?.Invoke(this);
    
}
