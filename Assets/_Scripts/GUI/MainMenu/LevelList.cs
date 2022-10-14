using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.GUI.MainMenu
{
    public class LevelList : UnityEngine.MonoBehaviour
    {
        //Dictionaries are a list of keys and values. A key points to a value.
        //Read more at: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-7.0
        public static Dictionary<GameObject, ILevel> LevelButtons = new();

        public GameObject ScrollViewerContent; //Content of ScrollViewer
        public GameObject ButtonPrefab; //Prefab for button instantiation

        private void OnEnable()
        {
            //Get all ILevel objects in project
            var levels = LevelManager.GetAllScenes();

            //for each level in levels, do the following
            foreach (var level in levels.OrderBy(x => x.LevelID))
            {
                var btn = CreateButtonReference(level); //Get reference to level in form of a button
                btn.transform.SetParent(ScrollViewerContent.transform, false); //Set transform
                LevelButtons.Add(btn, level); //Add button and level to dictionary
            }
        }

        private GameObject CreateButtonReference(ILevel level)
        {
            var btn = Instantiate(ButtonPrefab); //Instantiate prefab
            var button = btn.GetComponent<Button>(); //Get Button Component
            var text = button.GetComponentsInChildren<TextMeshProUGUI>(); //Get multiple text components

            text[0].text = level.LevelID.ToString();
            text[1].text = $"{level.Level.LevelName}"; //Set first text component
            text[2].text =
                $"'{level.Events.First().EventName}': {level.Events.First().Description}"; //Set second text component

            button.onClick.AddListener(LevelButtonClicked); //Add onClick Listener

            return btn; //Return button
        }

        private void LevelButtonClicked()
        {
            //Get reference to clicked button
            var button = EventSystem.current.currentSelectedGameObject;
            //Get level from dictionary
            var level = LevelButtons[button];

            //Load Scene
            LevelManager.LoadScene(level);
        }
    }
}