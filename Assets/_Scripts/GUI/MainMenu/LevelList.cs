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

        public GameObject ScrollViewerContent; //Content row 1
        public GameObject ScrollViewerContent2; //Content row 2
        public GameObject ButtonPrefab; //Prefab for button instantiation

        public bool _generated = false;

        private void OnEnable()
        {
            // Check if buttons weren't generated
            if (LevelButtons.Count > 0 && LevelButtons.ElementAt(0).Key == null)
            {
                // Instantiate Dictionary
                LevelButtons = new Dictionary<GameObject, ILevel>();
                // Set generated to false
                _generated = false;
            }

            // If levels were already generated, return
            if(_generated) return;
            if(LevelButtons.Count > 0 ) return;

            //Get all ILevel objects in project
            var levels = LevelManager.GetAllScenes();

            int i = 0; // Declare iterator

            //for each level in levels, do the following
            foreach (var level in levels.OrderBy(x => x.LevelID))
            {
                var btn = CreateButtonReference(level); //Get reference to level in form of a button
                if(i < 5)
                    btn.transform.SetParent(ScrollViewerContent.transform, false); //Set transform for row 1
                else
                    btn.transform.SetParent(ScrollViewerContent2.transform, false); //Set transform for row 2
                
                LevelButtons.Add(btn, level); //Add button and level to dictionary
                i++;
            }

            _generated = true; // Set generated to
        }

        private GameObject CreateButtonReference(ILevel level)
        {
            var btn = Instantiate(ButtonPrefab); //Instantiate prefab
            var button = btn.GetComponent<Button>(); //Get Button Component
            var sprites = button.GetComponent<AssetContainer>(); //Get multiple text components
            var numberImage = btn.transform.GetChild(0); // Image container for number
            var numberSprite = numberImage.GetComponent<Image>(); // Sprite container for number
            var numberSprites = numberImage.GetComponent<AssetContainer>().Sprites; // Number sprites

            button.onClick.AddListener(LevelButtonClicked); //Add onClick Listener
            button.interactable = false; // Deactivate button by default

            var unlockedLevels = InitializeLevelService.levels.UnlockedLevels; // Get all saved and unlocked levels
            
            numberSprite.sprite = numberSprites[level.LevelID]; // Assign sprite relative to level ID

            // Make sure level 0 is always unlocked
            if (level.LevelID == 0)
            {
                button.interactable = true; // Make button interactable
                button.image.sprite = sprites.Sprites[0]; // Assign 0 star sprite (will be changed later if not correct)

                SpriteState ss = new SpriteState(); // Instantiate new SpriteState
                
                ss.highlightedSprite = sprites.Sprites[4]; // Assign proper sprite
                ss.pressedSprite = sprites.Sprites[4]; // Assign proper sprite

                button.spriteState = ss; // Set button SpriteState to ss variable
            }
            
            // Loop through all unlocked levels
            foreach (var levelSave in unlockedLevels.Where(uLevel => uLevel.SceneName == level.Level.SceneName))
            {
                button.interactable = true; // Make button interactable
                button.image.sprite = sprites.Sprites[levelSave.StarCount]; // Assign sprite based on star count

                SpriteState ss = new SpriteState(); // Instantiate new SpriteState
                
                ss.highlightedSprite = sprites.Sprites[levelSave.StarCount + 4]; // Assign proper sprite
                ss.pressedSprite = sprites.Sprites[levelSave.StarCount + 4]; // Assign proper sprite

                button.spriteState = ss; // Set button SpriteState to ss variable

                break; // I'm honestly not sure why this is here, but I dare not change it
            }

            // If button is not interactable, deactivate it to assign proper SpriteState
            if (!button.interactable)
                numberSprite.gameObject.SetActive(false);
            
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

        /// <summary>
        /// Unlock all levels (used under development)
        /// </summary>
        /// <param name="unlock">Unlock or Lock</param>
        public static void UnlockAllLevels(bool unlock)
        {
            var unlockedLevels = InitializeLevelService.levels.UnlockedLevels;

            foreach (var button in LevelButtons.Keys)
            {
                if (unlock)
                {
                    var numberImage = button.transform.GetChild(0);
                    var numberSprite = numberImage.GetComponent<Image>();
                    var sprites = button.GetComponent<AssetContainer>(); //Get multiple text components

                    numberImage.gameObject.SetActive(true);
                    button.GetComponent<Button>().interactable = true;
                    
                    SpriteState ss = new SpriteState();

                    ss.highlightedSprite = sprites.Sprites[0 + 4];
                    ss.pressedSprite = sprites.Sprites[0 + 4];

                    button.GetComponent<Button>().spriteState = ss;
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