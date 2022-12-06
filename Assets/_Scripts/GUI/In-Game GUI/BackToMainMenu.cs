using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Add click listener
        this.GetComponent<Button>().onClick.AddListener(LoadMenu);
    }

    void LoadMenu()
    {
        // Load main menu scene
        SceneManager.LoadScene("MenuTest");
    }
}
