using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Timer_Handler : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float roundTime = 10f;
    private float currTime = 0f;
    private bool timerActive = false;
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        currTime = roundTime;
    }

    public void StartTimer() => timerActive = true;

    public void StopTimer() => timerActive = false;

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            if (currTime < 0)
            {
                timerActive = false;
                OnTimerDepleted();
                return;
            };

            TimeSpan time = new TimeSpan();
            var timeSpan = time.Add(TimeSpan.FromSeconds(currTime));

            text.text = $"{timeSpan.Minutes}:{timeSpan.Seconds}";

            currTime -= Time.deltaTime;
        }
        
    }

    private void OnTimerDepleted()
    {
        TimerDepletedEventHandler handler = TimerDepleted;
        handler?.Invoke(this);
    }
    
    public event TimerDepletedEventHandler TimerDepleted;
    public delegate void TimerDepletedEventHandler(object sender);
}
