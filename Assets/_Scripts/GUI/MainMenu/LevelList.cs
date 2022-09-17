using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Interfaces;
using TMPro;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GUI.MainMenu
{
    public class LevelList : UnityEngine.MonoBehaviour
    {
        public static Dictionary<GameObject, ILevel> LevelButtons = new Dictionary<GameObject, ILevel>();

        public GameObject ScrollViewerContent;
        public GameObject ButtonPrefab;
        private void OnEnable()
        {
            print("enabled");
            List<ILevel> levels = LevelManager.GetAllScenes();

            foreach (var level in levels.OrderBy(x=> x.LevelID))
            {
                var btn = CreateButtonReference(level);
                btn.transform.SetParent(ScrollViewerContent.transform, false);
                LevelButtons.Add(btn, level);
            }
        }

        private GameObject CreateButtonReference(ILevel level)
        {
            GameObject btn = Instantiate(ButtonPrefab);
            var button = btn.GetComponent<Button>();
            var text = button.GetComponentInChildren<TextMeshProUGUI>();

            text.text = $"{level.Level.SceneName} (ID {level.LevelID})";

            button.onClick.AddListener(LevelButtonClicked);
            
            return btn;
        }

        private void LevelButtonClicked()
        {
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            ILevel level = LevelButtons[button];
            
            print(level.Level.LevelScene.name);
            
            LevelManager.LoadScene(level);
        }
    }
}