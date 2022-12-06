using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        // Add click listener
        this.GetComponent<Button>().onClick.AddListener(ShowMenu);
    }

    void ShowMenu()
    {
        // Get and stop timer
        var ui = PlayerInteractionHandler.SceneObjects.UI;
        ui.Timer.TimerHandler.StopTimer();
        
        // Activate Menu
        Menu.SetActive(true);
    }
}
