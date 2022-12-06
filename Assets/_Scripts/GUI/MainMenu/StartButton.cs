using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        // Add click listener
        this.GetComponent<Button>().onClick.AddListener(EnableMainMenu);
    }

    void EnableMainMenu()
    {
        this.gameObject.SetActive(false); // Disable underlying game object
        mainMenu.SetActive(true); // Enable main menu
    }
}
