using System;
using System.Linq;
using _Scripts.Handlers;
using TMPro;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    /// <summary>
    /// Script for assigning objects that are supposed to make noise.
    /// Might have been more convenient to make the script assignable to objects that are NOT supposed to make noise.
    /// </summary>
    public class NoiseObject : UnityEngine.MonoBehaviour
    {
        public float noiseMultiplyFactor = 0.5f;
        private TextMeshProUGUI text; // debug text
        private const float DivisionFactor = 23f; // maths
        
        private void Start()
        {
            // get debug text
            text = PlayerInteractionHandler.SceneObjects.UI.DebugGUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Add noise on player collision
            if (collision.gameObject.CompareTag("Player"))
            {
                // Invoke collision event
                PlayerInteractionHandler.Self.OnCollidedInvoke();
                // Maths
                var noise = collision.relativeVelocity.magnitude / DivisionFactor;

                // Debug text
                text.text = $"Last hit noise: {noise * noiseMultiplyFactor:##.###}";
                
                // Add noise
                PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script.AddNoise(noise * noiseMultiplyFactor);
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    #if !UNITY_WEBGL
                    Handheld.Vibrate(); // Vibrate
                    #endif
                }
            }
        }
    }
}