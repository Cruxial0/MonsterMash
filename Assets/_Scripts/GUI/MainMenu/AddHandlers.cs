using System.Collections;
using System.Collections.Generic;
using _Scripts.GUI.MainMenu;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AddHandlers : MonoBehaviour
{
    public Button loadSceneButton;
    public GameObject LevelList;

    private LevelSelectButton _levelSelectButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _levelSelectButton = new LevelSelectButton();
        
        var button = loadSceneButton.GetComponent<Button>();
        //button.onClick.AddListener(LevelManager.LoadScene);
        button.onClick.AddListener(ToLevelSelect);
    }

    void ToLevelSelect() => _levelSelectButton.ToLevelList(this.gameObject, LevelList);
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
