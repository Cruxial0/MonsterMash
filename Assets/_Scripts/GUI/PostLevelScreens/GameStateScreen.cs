using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.GUI.PostLevelScreens
{
    public class GameStateScreen
    {
        private int starCount = 0;
        /// <summary>
        /// Generates a loss screen
        /// </summary>
        /// <returns>Loss screen</returns>
        public GameObject RestartLevelScreen()
        {
            // Get UI prefab
            var restartMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            // Assign sprite to background
            restartMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                restartMenu.GetComponent<AssetContainer>().Sprites[4];
            
            // Set continue button to be non-interactable
            restartMenu.transform.GetChild(3).GetComponent<Button>().interactable = false;

            return restartMenu;
        }

        public GameObject WinLevelScreen(int stars)
        {
            // Assign local variable
            starCount = stars;
            // Get UI prefab
            var winMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            // Assign sprite to background
            winMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                winMenu.GetComponent<AssetContainer>().Sprites[stars];

            // Check if there are 2 or more stars
            if (stars >= 2)
            {
                // Set the continue button to be interactable
                var btn = winMenu.transform.GetChild(3).GetComponent<Button>();
                btn.interactable = true;
            }

            return winMenu;
        }
        
        
    }
}