using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Button; //Reference to button

    private void Start()
    {
        //Add onClick Listener
        Button.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
        //Exit game
    }
}