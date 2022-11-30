using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extensions;
using _Scripts.Handlers;
using _Scripts.MonoBehaviour.Interactables.Traps;
#if UNITY_EDITOR
using _Scripts.MonoBehaviour.CommonFunctionality.Editors;
#endif
using _Scripts.MonoBehaviour.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    /// <summary>
    /// Universal script for adding sound to objects
    /// </summary>
    public class SoundObject : UnityEngine.MonoBehaviour
    {
        public Audio source;
        public SoundType soundType;

        [HideInInspector] public AudioSource _audioSource; // Associated AudioSource
        [HideInInspector] public string SelectedTag = ""; // Selected tag in inspector GUI
        [HideInInspector] public PlayerState SelectedStates = PlayerState.None; // Selected PlayerState
        [HideInInspector] public int MinInterval; // Min interval for sound loops
        [HideInInspector] public int MaxInterval; // Max interval for sound loops

        private GameObject _audioSourceContainer;
        
        private bool _moving; // Player moving?
        private bool _cycle; // Cycle mode enabled?
        private bool _buffed; // Player buffed?
        
        private float _currTime; // current time for intervals
        private float _interval; // current interval max time (based on rng between min and max)
        
        // Selected SoundType
        public enum SoundType
        {
            Collision,
            Cycle,
            PlayerState,
            Music
        }

        private void Start()
        {
            //Create container for AudioSource
            _audioSourceContainer = new GameObject($"{name}_SoundContainer")
            {
                transform =
                {
                    position = this.transform.position
                }
            };

            //Append new AudioSource
            _audioSource = CreateOrGetContainer(_audioSourceContainer).AddComponent<AudioSource>();
            try
            {
                ToAudioSource(source, _audioSource); // Converts Audio to AudioSource
            }
            catch (Exception e)
            {
                // If an error is found, no audio source was found, as all other properties are nullable
                Debug.LogError("No audio source was found."); 
                return;
            }
            
            DetermineSound(); // Handle sound based on SoundType
        }

        /// <summary>
        /// Parents object to an empty container, and returns object
        /// </summary>
        /// <param name="objectToAssign">Object to assign to parent object</param>
        /// <returns>objectToAssign</returns>
        private GameObject CreateOrGetContainer(GameObject objectToAssign)
        {
            GameObject container;
            if (GameObject.Find("SoundContainers") == null) container = new GameObject("SoundContainers");
            else container = GameObject.Find("SoundContainers");
            
            objectToAssign.transform.SetParent(container.transform);

            return objectToAssign;
        }
        
        private void DetermineSound()
        {
            // Switch on SoundType
            switch (soundType)
            {
                case SoundType.Collision:
                    // Check whether object is a trap or furniture
                    if (this.CompareTag("Trap"))
                    {
                        // Get current TrapObject through Linq expression
                        var trap = PlayerInteractionHandler.Self.TrapHandler.Interactibles.First(x => x.Parent.name == name);
                        // Subscribe to event
                        trap.TrapCollisionAdded += TrapOnTrapCollisionAdded;
                        break;
                    }
                    // Get current InteractableObject through Linq expression
                    var obj = PlayerInteractionHandler.Self.InteractableHandler.Interactibles.First(x => x.Parent.name == name);
                    // Subscribe to event
                    obj.CollisionAdded += OnCollisionAdded;
                    break;
                case SoundType.Cycle:
                    _interval = Random.Range(MinInterval, MaxInterval); // Get random cycle time
                    _cycle = true; // Activate cycle mode
                    break;
                case SoundType.PlayerState:
                    ManageEvents(); // Redirect to ManageEvents method
                    break;
                case SoundType.Music:
                    _audioSource.Play(); // Play audio
                    break;
            }
        }

        private void TrapOnTrapCollisionAdded(object sender, TrapEventArgs e)
        {
            // Return if collision tag does not match SelectedTag
            if (!e.TriggerEvent.gameObject.CompareTag(SelectedTag)) return;
            _audioSource.clip = source.GetRandomClip(); // Set clip to random clip
            _audioSource.Play(); // Play audio
        }

        private void OnCollisionAdded(object sender, CollisionEventArgs e)
        {
            // Return if collision tag does not match SelectedTag
            if (!e.CollisionEvent.collider.CompareTag(SelectedTag)) return;
            _audioSource.clip = source.GetRandomClip(); // Set clip to random clip
            _audioSource.Play(); // Play audio
        }

        /// <summary>
        /// Manages audio based on PlayerStates
        /// </summary>
        private void ManageEvents()
        {
            switch (SelectedStates)
            {
                // Apply lambda expression for _moving property
                case PlayerState.Moving:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerMoving += ctx => _moving = ctx;
                    break;
                // Subscribe to event with method assignment
                case PlayerState.Dead:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.OnPlayerDestroyed += PlayerStatesOnOnPlayerDestroyed;
                    break;
                // Apply lambda expression for _buffed property
                case PlayerState.Buffed:
                    PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerBuffed += ctx => _buffed = ctx;
                    break;
            }
        }
        

        // Play audio
        private void PlayerStatesOnOnPlayerDestroyed(bool destroyed) => _audioSource.Play();

        private void FixedUpdate()
        {
            // Return if AudioSource is playing to avoid overlapping audio
            if (_audioSource.isPlaying) return;
            
            // If cycle mode is enabled
            if (_cycle)
            {
                _currTime += Time.deltaTime; // Increment time
                if(_currTime < _interval) return; // Return if interval is less than current time
                _audioSource.clip = source.GetRandomClip(); // Get random clip
                _audioSource.Play(); // Play random clip
            }
            
            if (_moving && !_audioSource.isPlaying)
                _audioSource.Play(); // Play audio if _moving is true

            if (_buffed && !_audioSource.isPlaying)
                _audioSource.Play(); // Play audio if _buffed is true

        }

        /// <summary>
        /// Converts internal Audio class to AudioSource
        /// </summary>
        /// <param name="source">Audio to convert from</param>
        /// <param name="audioSource">Target AudioSource</param>
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
        
        /// <summary>
        /// Generates Audio from AudioSource
        /// </summary>
        /// <param name="audioSource">Audio source to convert from</param>
        /// <returns></returns>
        public Audio FromAudioSource(AudioSource audioSource)
        {
            Audio source = new Audio();
            source.soundClips = this.source.soundClips;
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

        /// <summary>
        /// Gets a random audio clip
        /// </summary>
        /// <param name="clips"></param>
        /// <returns>Random AudioClip</returns>
        private AudioClip RandomAudioClip(List<AudioClip> clips)
        {
            return clips[Random.Range(0, clips.Count)];
        }
    }

    /// <summary>
    /// Custom container for AudioSource essentials
    /// </summary>
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
