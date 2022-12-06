using System;
using System.Linq;
using _Scripts.Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerHandler : MonoBehaviour
{
    public delegate void TimerDepletedEventHandler(object sender);

    public float roundTime = 100f; //Round time
    [NonSerialized] public float currTime; //Current time
    private TextMeshProUGUI text; //Instance of Text
    private bool timerActive; //Timer active?
    [NonSerialized] public bool TimerRun = false;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); //Get Text component and assign it
        roundTime = LevelManager.GetAllScenes().First(x => x.Level.SceneName == SceneManager.GetActiveScene().name)
            .LevelTime;
        currTime = roundTime; //Set Current Time to Round Time
        
        SetTime();

        // Lock bed when timer is running
        PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.OnBedExit += () => TimerRun = true;
    }

    // Update is called once per frame
    private void Update()
    {
        //If Timer is active
        if (!timerActive || !TimerRun) return;
        //If Current Time is less than 0
        if (currTime < 0)
        {
            //Disable timer
            timerActive = false;
            OnTimerDepleted(); //Call event function
            return;
        }

        SetTime();

        currTime -= Time.deltaTime; //Decrease time by Time.deltaTime
    }

    /// <summary>
    /// Sets formatted time
    /// </summary>
    private void SetTime()
    {
        var time = new TimeSpan(); //Get new instance of TimeSpan
        var timeSpan = time.Add(TimeSpan.FromSeconds(currTime)); //TimeSpan += currTime

        text.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}"; //Format text
    }
    
    //Starts Timer
    public void StartTimer() => timerActive = true;

    //Stops Timer
    public void StopTimer() => timerActive = false;

    //Event function for when timer depletes
    //Learn more about events at: https://learn.microsoft.com/en-us/dotnet/standard/events/
    private void OnTimerDepleted()
    {
        var handler = TimerDepleted;
        handler?.Invoke(this);
    }

    public event TimerDepletedEventHandler TimerDepleted;
}