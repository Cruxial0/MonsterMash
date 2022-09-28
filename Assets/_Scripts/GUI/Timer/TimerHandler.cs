using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    private TextMeshProUGUI text; //Instance of Text
    public float roundTime = 100f; //Round time
    private float currTime = 0f; //Current time
    private bool timerActive = false; //Timer active?
    
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>(); //Get Text component and assign it
        currTime = roundTime; //Set Current Time to Round Time
    }

    //Starts Timer
    public void StartTimer() => timerActive = true;

    //Stops Timer
    public void StopTimer() => timerActive = false;

    // Update is called once per frame
    void Update()
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
            };
            
            TimeSpan time = new TimeSpan(); //Get new instance of TimeSpan
            var timeSpan = time.Add(TimeSpan.FromSeconds(currTime)); //TimeSpan += currTime

            text.text = $"{timeSpan.Minutes}:{timeSpan.Seconds:00}"; //Format text

            currTime -= Time.deltaTime; //Decrease time by Time.deltaTime
        }
    }

    //Event function for when timer depletes
    //Learn more about events at: https://learn.microsoft.com/en-us/dotnet/standard/events/
    private void OnTimerDepleted()
    {
        TimerDepletedEventHandler handler = TimerDepleted;
        handler?.Invoke(this);
    }
    public event TimerDepletedEventHandler TimerDepleted;
    public delegate void TimerDepletedEventHandler(object sender);
}
