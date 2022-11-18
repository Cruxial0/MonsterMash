using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extensions;
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

        [HideInInspector] public AudioSource _audioSource;
        [HideInInspector] public string SelectedTag = "";
        [HideInInspector] public bool OnlyNoiseObject;
        [HideInInspector] public PlayerState SelectedStates = PlayerState.None;
        [HideInInspector] public int MinInterval;
        [HideInInspector] public int MaxInterval;

        private bool _moving;
        private bool _cycle;
        private bool _buffed;
        
        private float _currTime;
        private float _interval;
        
        public enum SoundType
        {
            Collision,
            Cycle,
            PlayerState,
            Music
        }

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            try
            {
                ToAudioSource(source, _audioSource);
            }
            catch (Exception e)
            {
                Debug.LogError("No audio source was found.");
                throw;
            }
            
            DetermineSound();
        }

        private void DetermineSound()
        {
            switch (soundType)
            {
                case SoundType.Collision:
                    PlayerInteractionHandler.SceneObjects.Room.FurnitureObjects.First(x => x.Transform == transform).Script.OnCollisionDetected += ScriptOnOnCollisionDetected;
                    break;
                case SoundType.Cycle:
                    _interval = Random.Range(MinInterval, MaxInterval);
                    _cycle = true;
                    break;
                case SoundType.PlayerState:
                    ManageEvents();
                    break;
                case SoundType.Music:
                    _audioSource.Play();
                    break;
            }
        }

        private void ManageEvents()
        {
            switch (SelectedStates)
            {
                case PlayerState.Moving:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerMoving += ctx => _moving = ctx;
                    break;
                case PlayerState.Dead:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.OnPlayerDestroyed += PlayerStatesOnOnPlayerDestroyed;
                    break;
                case PlayerState.Buffed:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerBuffed += ctx => _buffed = ctx;
                    break;
                case PlayerState.Collided:
                    break;
            }
        }
        

        private void PlayerStatesOnOnPlayerDestroyed(bool destroyed) => _audioSource.Play();

        private void ScriptOnOnCollisionDetected(Collision c)
        {
            //UnityEditorInternal.InternalEditorUtility.tags[SelectedTag]
            #if UNITY_EDITOR
            if (c.collider.CompareTag(SelectedTag))
            {
                _audioSource.clip = source.GetRandomClip();
                _audioSource.Play();
            }
            #endif
        }

        private void FixedUpdate()
        {
            if (_cycle)
            {
                _currTime += Time.deltaTime;
                if(_currTime < _interval) return;
                _audioSource.clip = source.GetRandomClip();
                _audioSource.Play();
            }
            
            if (_moving) _audioSource.Play();
            if (_buffed)
            {
                _audioSource.Play();
            }
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

        public Audio FromAudioSource(AudioSource audioSource)
        {
            Audio source = new Audio(); 
            source.outputMixerGroup = audioSource.outputAudioMixerGroup;
            source.mute = audioSource.mute;
            source.bypassEffects = audioSource.bypassEffects;
            source.bypassListenerEffects = audioSource.bypassListenerEffects;
            source.bypassReverbZones = audioSource.bypassReverbZones;
            source.playOnAwake = audioSource.playOnAwake;
            source.loop = audioSource.loop;
            source.priority = audioSource.priority;
            source.volume = audioSource.volume;
            source.pitch = audioSource.pitch;
            source.stereoPan = audioSource.panStereo;
            source.spatialBlend = audioSource.spatialBlend;
            source.reverbZoneMix = audioSource.reverbZoneMix;
            source.volumeRolloff = audioSource.rolloffMode;
            return source;
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
