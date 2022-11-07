using System;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.GUI.NoiseMeter
{
    public class NoiseMeter : UnityEngine.MonoBehaviour
    {
        public Slider slider;
        public Gradient gradient;
        public Image fill;
        [NonSerialized] public bool IsMute = false;

        public void AddNoise(float noise)
        {
            if(IsMute) return;
            
            slider.value += noise;

            if (slider.value >= slider.maxValue)
            {
                PlayerInteractionHandler.GameStateManager.Lose(LoseCondition.Noise);
            }
            
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
