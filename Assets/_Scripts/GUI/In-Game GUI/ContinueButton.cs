using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject Menu;
    void Start()
    {
        // Add click listener
        this.GetComponent<Button>().onClick.AddListener(ShowMenu);
    }

    void ShowMenu()
    {
        // Gets and starts the timer
        var ui = PlayerInteractionHandler.SceneObjects.UI;
        ui.Timer.TimerHandler.StartTimer();
        
        // Deactivate Menu
        Menu.SetActive(false);
    }
}
