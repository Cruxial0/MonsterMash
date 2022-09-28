using System.Collections;
using System.Collections.Generic;
using _Scripts.GUI.MainMenu;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AddHandlers : MonoBehaviour
{
    public Button loadSceneButton; //Reference to level button
    public GameObject LevelList; //Canvas

    private LevelSelectButton _levelSelectButton; //Instance of LevelSelectButton class

    // Start is called before the first frame update
    void Start()
    {
        _levelSelectButton = new LevelSelectButton(); //Instantiate LevelSelectButton
        
        var button = loadSceneButton.GetComponent<Button>(); //Get Button component
        button.onClick.AddListener(ToLevelSelect); //Add onClick Listener
    }

    //Swap view to level list
    void ToLevelSelect() => _levelSelectButton.ToLevelList(this.gameObject, LevelList);
}
