using System;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GUI.NoiseMeter
{
    public class NoiseMeter : UnityEngine.MonoBehaviour
    {
        public Slider slider; // Slider
        public Gradient gradient; // Gradient used for color assignment
        public Image fill; // Slider Fill
        [NonSerialized] public bool IsMute = false;

        /// <summary>
        /// Adds noise to the noise meter.
        /// </summary>
        /// <param name="noise">Amount of noise to add</param>
        public void AddNoise(float noise)
        {
            if(IsMute) return; // If player has mute power, return
            
            slider.value += noise;

            // Check if value is more than maxValue
            if (slider.value >= slider.maxValue)
            {
                // Lose
                PlayerInteractionHandler.GameStateManager.Lose(LoseCondition.Noise);
            }
            
            // Get correct color for noise meter
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
