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
        this.GetComponent<Button>().onClick.AddListener(EnableMainMenu);
    }

    void EnableMainMenu()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
