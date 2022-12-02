using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers;
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
        public GameObject ScrollViewerContent2;
        public GameObject ButtonPrefab; //Prefab for button instantiation

        public bool _generated = false;

        private void OnEnable()
        {
            if (LevelButtons.Count > 0 && LevelButtons.ElementAt(0).Key == null)
            {
                LevelButtons = new Dictionary<GameObject, ILevel>();
                _generated = false;
            }
            if(_generated) return;
            //Get all ILevel objects in project
            var levels = LevelManager.GetAllScenes();

            if(LevelButtons.Count > 0 ) return;

            int i = 0;
            //for each level in levels, do the following
            foreach (var level in levels.OrderBy(x => x.LevelID))
            {
                var btn = CreateButtonReference(level); //Get reference to level in form of a button
                if(i < 5)
                    btn.transform.SetParent(ScrollViewerContent.transform, false); //Set transform
                else
                    btn.transform.SetParent(ScrollViewerContent2.transform, false); //Set transform
                
                LevelButtons.Add(btn, level); //Add button and level to dictionary
                i++;
            }

            _generated = true;
        }

        private GameObject CreateButtonReference(ILevel level)
        {
            var btn = Instantiate(ButtonPrefab); //Instantiate prefab
            var button = btn.GetComponent<Button>(); //Get Button Component
            var sprites = button.GetComponent<AssetContainer>(); //Get multiple text components
            var numberImage = btn.transform.GetChild(0);
            var numberSprite = numberImage.GetComponent<Image>();
            var numberSprites = numberImage.GetComponent<AssetContainer>().Sprites;

            button.onClick.AddListener(LevelButtonClicked); //Add onClick Listener
            button.interactable = false;
            var unlockedLevels = InitializeLevelService.levels.UnlockedLevels;
            
            numberSprite.sprite = numberSprites[level.LevelID];

            foreach (var levelSave in unlockedLevels.Where(uLevel => uLevel.SceneName == level.Level.SceneName))
            {
                button.interactable = true;
                button.image.sprite = sprites.Sprites[levelSave.StarCount];
                
                break;
            }

            if (!button.interactable)
            {
                //button.image.sprite = sprites.Sprites[4];
                numberSprite.gameObject.SetActive(false);
            }
            
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

        public static void UnlockAllLevels(bool unlock)
        {
            var unlockedLevels = InitializeLevelService.levels.UnlockedLevels;

            foreach (var button in LevelButtons.Keys)
            {
                if (unlock)
                {
                    var numberImage = button.transform.GetChild(0);
                    var numberSprite = numberImage.GetComponent<Image>();
                    numberImage.gameObject.SetActive(true);
                    button.GetComponent<Button>().interactable = true;
                }
                else
                {
                    var level = LevelButtons[button];
                    
                    if (unlockedLevels.Count == 0)
                    {
                        button.GetComponent<Button>().interactable = false;
                        continue;
                    }
                    
                    if (unlockedLevels.Count(uLevel => uLevel.SceneName == level.Level.SceneName) > 0)
                    {
                        button.GetComponent<Button>().interactable = true;
                        continue;
                    }
                    
                    var numberImage = button.transform.GetChild(0);
                    var numberSprite = numberImage.GetComponent<Image>();
                    numberImage.gameObject.SetActive(false);
                    button.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}