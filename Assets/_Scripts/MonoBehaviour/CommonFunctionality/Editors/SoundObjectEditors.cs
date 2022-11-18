#if UNITY_EDITOR
using System;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.MonoBehaviour.Player;
using UnityEditor;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality.Editors
{
    [CustomEditor(typeof(SoundObject))]
    public class SoundObjectEditors : Editor
    {
        SerializedObject _editorObject;

        private void OnEnable()
        {
            _editorObject = new SerializedObject(target);
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
           // EditorUtility.SetDirty(this);

            switch (sound.soundType)
            {
                case SoundObject.SoundType.Collision:
                    GUILayout.Space(5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Collision Tag");
                    sound.SelectedTag = EditorGUILayout.TextField(sound.SelectedTag);
                    if (GUILayout.Button("Tag List"))
                    {
                        EditorUtility.DisplayDialog("Unity Tags",
                            String.Join(Environment.NewLine, UnityEditorInternal.InternalEditorUtility.tags),
                            "boing jumpscare");
                    }
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
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Player State");
                    sound.SelectedStates = (PlayerState)EditorGUILayout.EnumFlagsField(sound.SelectedStates);
                    GUILayout.EndHorizontal();
                    break;
            }
            
            EditorGUI.EndChangeCheck();
            
            GUILayout.Space(20f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Update values"))
            {
                sound.source = sound.FromAudioSource(sound._audioSource);
            }
            
            if (GUILayout.Button("Evaluate"))
            {
                if (sound.source.soundClips.Count == 0)
                {
                    Debug.LogWarning("EVALUATE: Please assign a valid AudioClip to source.");
                    return;
                }

                if (!UnityEditorInternal.InternalEditorUtility.tags.Contains(sound.SelectedTag) && sound.SelectedTag != String.Empty)
                {
                    Debug.LogWarning("Tag does not exist.");
                }
                switch (sound.soundType)
                {
                    case SoundObject.SoundType.Cycle when sound.MaxInterval == 0:
                        Debug.LogWarning("Consider setting a max interval timer.");
                        break;
                    case SoundObject.SoundType.PlayerState when sound.SelectedStates == PlayerState.None:
                        Debug.LogWarning("Consider setting a player state.");
                        break;
                    case SoundObject.SoundType.Collision when sound.SelectedTag == string.Empty:
                        Debug.LogWarning("Consider setting a collision tag.");
                        break;
                    default:
                        Debug.Log("Everything seems okay.");
                        return;
                }
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
            //base.OnGUI(position, property, label);

            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.PropertyField(property.FindPropertyRelative("soundClips"));

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

            EditorGUI.EndProperty();
            
            GUILayout.Space(20f);
        }
    }

    [CustomPropertyDrawer(typeof(ParentSounds))]
    public class ParentSoundsUIE : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);

            EditorGUILayout.PropertyField(property.FindPropertyRelative("approachSounds"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("enterSounds"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("exitSounds"));
            
            EditorGUI.EndProperty();
        }
    }
}
#endif
