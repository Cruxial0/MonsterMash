using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Add click listener
        this.GetComponent<Button>().onClick.AddListener(RestartLevel);
    }

    void RestartLevel()
    {
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
