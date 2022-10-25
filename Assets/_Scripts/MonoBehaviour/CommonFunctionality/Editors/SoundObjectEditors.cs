using System;
using _Scripts.Handlers;
using _Scripts.MonoBehaviour.Player;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts.MonoBehaviour.CommonFunctionality.Editors
{
    [CustomEditor(typeof(SoundObject))]
    public class SoundObjectEditors : Editor
    {
        SerializedObject _editorObject;

        private void OnEnable()
        {
            _editorObject = new SerializedObject (target);
        }

        /// <summary>
        /// Good luck figuring out what this does. I'm not commenting this shit.
        /// It's just GUI though.
        /// </summary>
        public override void OnInspectorGUI()
        {
            _editorObject.Update();
            base.DrawDefaultInspector();
            
            SoundObject sound = (SoundObject)target;

            EditorGUI.BeginChangeCheck();
            
            switch (sound.soundType)
            {
                case SoundObject.SoundType.Collision:
                    GUILayout.Space(5f);
                    sound.OnlyNoiseObject = GUILayout.Toggle(sound.OnlyNoiseObject, "Only NoiseObjects?");
                    GUILayout.Space(2f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Collision Tag");
                    sound.SelectedTag = EditorGUILayout.Popup(sound.SelectedTag, UnityEditorInternal.InternalEditorUtility.tags);
                    GUILayout.EndHorizontal();
                    break;
                case SoundObject.SoundType.Cycle:
                    GUILayout.Label("Interval", EditorStyles.boldLabel);
                    GUILayout.BeginHorizontal(new[]{GUILayout.Width(71)});
                    GUILayout.Label("Min");
                    sound.MinInterval = int.Parse(GUILayout.TextField(sound.MinInterval.ToString(), new[]{GUILayout.Width(40)}));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(new[]{GUILayout.Width(60)});
                    GUILayout.Label("Max");
                    sound.MaxInterval = int.Parse(GUILayout.TextField(sound.MaxInterval.ToString(), new[]{GUILayout.Width(40)}));
                    GUILayout.EndHorizontal();
                    break;
                case SoundObject.SoundType.PlayerState:
                    sound.SelectedStates = (PlayerState)EditorGUILayout.EnumPopup("State", sound.SelectedStates);
                    break;
            }
            EditorUtility.SetDirty(this);
            EditorGUI.EndChangeCheck();
            
            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Play sound"))
            {
                sound._audioSource.Play();
            }

            if (GUILayout.Button("Evaluate"))
            {
                if (sound.source.source == null)
                    Debug.LogWarning("EVALUATE: Please assign a valid AudioClip to source.");
            }
            GUILayout.EndHorizontal();
            _editorObject.ApplyModifiedProperties();
        }
    }

    [CustomPropertyDrawer(typeof(Audio))]
    public class AudioSourceUIE : PropertyDrawer
    {
        bool foldout;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("source"));
            
            EditorGUI.BeginChangeCheck ();
            EditorGUILayout.GetControlRect (true, 16f, EditorStyles.foldout);
            Rect foldRect = GUILayoutUtility.GetLastRect ();
            if (Event.current.type == EventType.MouseUp && foldRect.Contains (Event.current.mousePosition)) {
                foldout = !foldout;
                //GUI.changed = true;
                Event.current.Use ();
            }
            foldout = EditorGUI.Foldout (foldRect, foldout, "Advanced");
            if (EditorGUI.EndChangeCheck ()) { }
 
            if (foldout) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(property.FindPropertyRelative("outputMixerGroup"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("mute"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("bypassEffects"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("playOnAwake"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("loop"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("priority"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("volume"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("pitch"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("stereoPan"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("spatialBlend"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("reverbZoneMix"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("volumeRolloff"));
                EditorGUI.indentLevel--;
            }
            
            GUILayout.Space(20f);
            
            EditorGUI.EndProperty();
        }
    }
}
