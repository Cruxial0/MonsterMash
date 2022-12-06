using _Scripts.GUI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

public class AddHandlers : MonoBehaviour
{
    public Button loadSceneButton; //Reference to level button
    public GameObject LevelList; //Canvas

    private LevelSelectButton _levelSelectButton; //Instance of LevelSelectButton class

    // Start is called before the first frame update
    private void Start()
    {
        _levelSelectButton = new LevelSelectButton(); //Instantiate LevelSelectButton

        //var button = loadSceneButton.GetComponent<Button>(); //Get Button component
        loadSceneButton.onClick.AddListener(ToLevelSelect); //Add onClick Listener
    }

    //Swap view to level list
    private void ToLevelSelect()
    {
        // Hide menu, and show level select
        _levelSelectButton.ToLevelList(gameObject, LevelList);
    }
}