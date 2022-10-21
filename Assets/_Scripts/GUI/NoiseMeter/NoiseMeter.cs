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

        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);
        }

        public void AddNoise(float noise)
        {
            slider.value += noise;

            if (slider.value >= slider.maxValue)
            {
                PlayerInteractionHandler.GameStateManager.Lose();
            }
            
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }
}
