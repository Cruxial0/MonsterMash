using System;
using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    public delegate void TimerDepletedEventHandler(object sender);

    public float roundTime = 100f; //Round time
    [NonSerialized] public float currTime; //Current time
    private TextMeshProUGUI text; //Instance of Text
    private bool timerActive; //Timer active?

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>(); //Get Text component and assign it
        currTime = roundTime; //Set Current Time to Round Time
    }

    // Update is called once per frame
    private void Update()
    {
        //If Timer is active
        if (timerActive)
        {
            //If Current Time is less than 0
            if (currTime < 0)
            {
                //Disable timer
                timerActive = false;
                OnTimerDepleted(); //Call event function
                return;
            }

            var time = new TimeSpan(); //Get new instance of TimeSpan
            var timeSpan = time.Add(TimeSpan.FromSeconds(currTime)); //TimeSpan += currTime

            text.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}"; //Format text

            currTime -= Time.deltaTime; //Decrease time by Time.deltaTime
        }
    }

    //Starts Timer
    public void StartTimer()
    {
        timerActive = true;
    }

    //Stops Timer
    public void StopTimer()
    {
        timerActive = false;
    }

    //Event function for when timer depletes
    //Learn more about events at: https://learn.microsoft.com/en-us/dotnet/standard/events/
    private void OnTimerDepleted()
    {
        var handler = TimerDepleted;
        handler?.Invoke(this);
    }

    public event TimerDepletedEventHandler TimerDepleted;
}