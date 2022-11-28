#if UNITY_EDITOR // This excludes this file from any other build platform other than the unity editor.
                 // This prevents some build errors when building for Android/WebGL
using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;
using UnityEditor;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Powers.Editors
{
    /// <summary>
    /// Custom Inspector GUI for Powers
    /// </summary>
    [CustomEditor(typeof(PowerInitialize))]
    public class PowerInitEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector(); // Draws base GUI
            
            PowerInitialize power = (PowerInitialize)target; // Gets associated instance of PowerInitialize
            
            // Marks this object as dirty. Allows you to make an object without an undo entry.
            EditorUtility.SetDirty(this); 
            
            GUILayout.Space(5f); // Adds 5px of transparent space
            GUILayout.BeginHorizontal(); // Begins a horizontal stack panel
            if (GUILayout.Button("Validate name")) // Creates a button
            {
                bool exists = GetAllPowers().Contains(power.powerName); // Check if powerName is equal to a valid power
                Debug.Log($"Power exists: {exists}"); // Log result
            }
            if (GUILayout.Button("List Powers")) // Lists all powers
            {
                EditorUtility
                    .DisplayDialog("All power names", 
                        string.Join(Environment.NewLine, GetAllPowers()), 
                            "Imagine reading these lmao");
            }
            GUILayout.EndHorizontal(); // End the horizontal stack panel
        }

        /// <summary>
        /// Makes use of reflection to get all Power scripts
        /// </summary>
        /// <returns>List of power names</returns>
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
#endif