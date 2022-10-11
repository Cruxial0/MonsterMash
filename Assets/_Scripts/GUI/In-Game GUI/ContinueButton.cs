using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ShowMenu);
    }

    void ShowMenu()
    {
        var ui = PlayerInteractionHandler.SceneObjects.UI;
        ui.Timer.TimerHandler.StartTimer();
        
        Menu.SetActive(false);
    }
}
