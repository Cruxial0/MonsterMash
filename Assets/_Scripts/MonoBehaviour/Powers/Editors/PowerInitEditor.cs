using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;
using UnityEditor;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Powers.Editors
{
    [CustomEditor(typeof(PowerInitialize))]
    public class PowerInitEditor : Editor
    {
        SerializedObject _editorObject;

        private void OnEnable()
        {
            _editorObject = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            
            EditorUtility.SetDirty(this);
            
            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("List Powers"))
            {
                EditorUtility.DisplayDialog("All power names", string.Join(Environment.NewLine, GetAllPowers()), "OK!");
            }
            GUILayout.EndHorizontal();
        }

        private List<string> GetAllPowers()
        {
            var Levels = new List<string>();

            var type = typeof(IPower); //type of ILevel
            //Get all instances of type ILevel in the project
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToArray();

            //Add instances of level to Levels
            foreach (var power in types)
            {
                if (!power.IsClass) continue;
                Levels.Add(((IPower)Activator.CreateInstance(power)).PowerName);
            }

            return Levels;
        }
    }
}