using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
#if UNITY_EDITOR
using _Scripts.MonoBehaviour.CommonFunctionality.Editors;
#endif
using _Scripts.MonoBehaviour.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class SoundObject : UnityEngine.MonoBehaviour
    {
        public Audio source;
        public SoundType soundType;

        [SerializeField, HideInInspector] public List<AudioClip> parentSounds;
        [HideInInspector] public AudioSource _audioSource;
        [HideInInspector] public int SelectedTag ;
        [HideInInspector] public bool OnlyNoiseObject;
        [HideInInspector] public PlayerState SelectedStates = PlayerState.None;
        [HideInInspector] public int MinInterval;
        [HideInInspector] public int MaxInterval;

        
        public enum SoundType
        {
            Collision,
            Cycle,
            PlayerState,
            Parents
        }

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            ToAudioSource(source, _audioSource);
            DetermineSound();
        }

        private void DetermineSound()
        {
            switch (soundType)
            {
                case SoundType.Collision:
                    print(gameObject.name);
                    PlayerInteractionHandler.SceneObjects.Room.FurnitureObjects.First(x => x.Transform == transform).Script.OnCollisionDetected += ScriptOnOnCollisionDetected;
                    break;
                case SoundType.Cycle:
                    break;
                case SoundType.PlayerState:
                    ManageEvents();
                    break;
                case SoundType.Parents:
                    PlayerInteractionHandler.SceneObjects.Room.DoorObject.Script.OnParentsApproach += ScriptOnOnParentsApproach;
                    PlayerInteractionHandler.SceneObjects.Room.DoorObject.Script.OnParentsEnter += ScriptOnOnParentsEnter;
                    PlayerInteractionHandler.SceneObjects.Room.DoorObject.Script.OnParentsLeave += ScriptOnOnParentsLeave;
                    break;
            }
        }

        private void ScriptOnOnParentsLeave(object sender)
        {
            //_audioSource.clip = RandomAudioClip(parentSounds.exitSounds);
            _audioSource.Play();
        }

        private void ScriptOnOnParentsEnter(object sender)
        {
            //_audioSource.clip = RandomAudioClip(parentSounds.exitSounds);
            _audioSource.Play();
        }

        private void ScriptOnOnParentsApproach(object sender)
        {
            //_audioSource.clip = RandomAudioClip(parentSounds.exitSounds);
            _audioSource.Play();
        }

        private void ManageEvents()
        {
            switch (PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerState)
            {
                case PlayerState.Moving:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerMoving += PlayerStatesOnPlayerMoving;
                    break;
                case PlayerState.Dead:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.OnPlayerDestroyed += PlayerStatesOnOnPlayerDestroyed;
                    break;
                case PlayerState.Buffed:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerBuffed += () => _audioSource.Play();
                    break;
                case PlayerState.Collided:
                    break;
            }
        }

        private void PlayerStatesOnPlayerMoving()
        {
            print("moving");
            _audioSource.Play();
        }

        private void PlayerStatesOnOnPlayerDestroyed(bool destroyed) => _audioSource.Play();

        private void ScriptOnOnCollisionDetected(Collision c)
        {
            #if UNITY_EDITOR
            if (c.collider.CompareTag(UnityEditorInternal.InternalEditorUtility.tags[SelectedTag]))
            {
                _audioSource.Play();
            }
            #endif
        }
        
        public void ToAudioSource(Audio source, AudioSource audioSource)
        {
            audioSource.clip = RandomAudioClip(source.soundClips);
            audioSource.outputAudioMixerGroup = source.outputMixerGroup;
            audioSource.mute = source.mute;
            audioSource.bypassEffects = source.bypassEffects;
            audioSource.bypassListenerEffects = source.bypassListenerEffects;
            audioSource.bypassReverbZones = source.bypassReverbZones;
            audioSource.playOnAwake = source.playOnAwake;
            audioSource.loop = source.loop;
            audioSource.priority = source.priority;
            audioSource.volume = source.volume;
            audioSource.pitch = source.pitch;
            audioSource.panStereo = source.stereoPan;
            audioSource.spatialBlend = source.spatialBlend;
            audioSource.reverbZoneMix = source.reverbZoneMix;
            audioSource.rolloffMode = source.volumeRolloff;
        }

        public AudioClip RandomAudioClip(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }
    }

    [Serializable]
    public class ParentSounds
    {
        public List<AudioClip> approachSounds;
        public List<AudioClip> enterSounds = new List<AudioClip>();
        public List<AudioClip> exitSounds = new List<AudioClip>();
    }
    
    [Serializable]
    public class Audio
    {
        public List<AudioClip> soundClips;
        public AudioRolloffMode volumeRolloff;
        public AudioMixerGroup outputMixerGroup;
        public bool mute;
        public bool bypassEffects;
        public bool bypassListenerEffects;
        public bool bypassReverbZones;
        public bool playOnAwake;
        public bool loop;
        [Range(0, 256)]
        public int priority = 128;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0f, 3f)]
        public float pitch = 1f;
        [Range(-1f, 1f)]
        public float stereoPan = 0f;
        [Range(0f, 1f)]
        public float spatialBlend = 0f;
        [Range(0f, 1.1f)]
        public float reverbZoneMix = 1f;

        
    }
}