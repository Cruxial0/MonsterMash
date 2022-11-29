using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers.SceneManagers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        var scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case "Tutorial":
                SceneManager.LoadScene("Level 1");
                return;
            case "Level 1":
                SceneManager.LoadScene("Level 2");
                return;
            case "Level 2":
                SceneManager.LoadScene("Level 3");
                return;
            case "Level 3":
                SceneManager.LoadScene("Level 4");
                return;
            case "Level 4":
                SceneManager.LoadScene("Level 5");
                return;
            case "Level 5":
                SceneManager.LoadScene("Level 6");
                return;
            case "Level 6":
                SceneManager.LoadScene("DarkLevel");
                return;
                    
        }  

        SceneManager.LoadScene("MenuTest");
    }
}
