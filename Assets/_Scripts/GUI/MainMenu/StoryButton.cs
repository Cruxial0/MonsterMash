using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class StoryButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Level0");
    }
}
