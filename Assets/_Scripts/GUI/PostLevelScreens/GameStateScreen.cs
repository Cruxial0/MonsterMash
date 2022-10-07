using _Scripts.Handlers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GUI.PostLevelScreens
{
    public class GameStateScreen
    {
        public GameObject RestartLevelScreen()
        {
            var restartMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            //Get text objects
            var textObjects = restartMenu.GetComponentsInChildren<TextMeshProUGUI>();

            //Format text
            // textObjects[0].text = "You lost!";
            // textObjects[0].color = Color.red;
            //
            // textObjects[1].text = "Restarting in 5 seconds...";
            // textObjects[1].color = Color.red;
            
            restartMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                restartMenu.GetComponent<AssetContainer>().Sprites[1];

            return restartMenu;
        }

        public GameObject WinLevelScreen()
        {
            var winMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            //Get text objects
            var textObjects = winMenu.GetComponentsInChildren<TextMeshProUGUI>();

            //Format text
            // textObjects[0].text = "You win!!";
            // textObjects[0].color = Color.green;
            //
            // textObjects[1].text = "Proceeding to next level...";
            // textObjects[1].color = Color.green;
            
            winMenu.transform.GetChild(0).GetComponent<Image>().sprite =
                winMenu.GetComponent<AssetContainer>().Sprites[0];

            return winMenu;
        }

        public GameObject ReturnToMenuScreenWin()
        {
            var winMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            //Get text objects
            var textObjects = winMenu.GetComponentsInChildren<TextMeshProUGUI>();

            //Format text
            textObjects[0].text = "You win!!";
            textObjects[0].color = Color.green;

            textObjects[1].text = "Returning to the menu in 5 seconds...";
            textObjects[1].color = Color.green;

            return winMenu;
        }

        public GameObject ReturnToMenuScreenLose()
        {
            var loseMenu = PlayerInteractionHandler.SceneObjects.Room.BedObject.Script.WinPrefab;

            //Get text objects
            var textObjects = loseMenu.GetComponentsInChildren<TextMeshProUGUI>();

            //Format text
            textObjects[0].text = "You lose!";
            textObjects[0].color = Color.red;

            textObjects[1].text = "Returning to the menu in 5 seconds...";
            textObjects[1].color = Color.red;

            return loseMenu;
        }
    }
}