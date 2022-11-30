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
        public GameObject RestartLevelScreen()
        {
            var restartMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            restartMenu.transform.GetChild(1).GetComponent<Image>().sprite =
                restartMenu.GetComponent<AssetContainer>().Sprites[4];
            
            restartMenu.transform.GetChild(4).GetComponent<Button>().interactable = false;

            return restartMenu;
        }

        public GameObject WinLevelScreen(int stars)
        {
            starCount = stars;
            var winMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            winMenu.transform.GetChild(1).GetComponent<Image>().sprite =
                winMenu.GetComponent<AssetContainer>().Sprites[stars];

            if (stars >= 2)
            {
                winMenu.transform.GetChild(4).GetComponent<Button>().interactable = true;
            }

            return winMenu;
        }
        
        
    }
}