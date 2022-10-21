using System;
using System.Linq;
using _Scripts.Handlers;
using TMPro;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class NoiseObject : UnityEngine.MonoBehaviour
    {
        private TextMeshProUGUI text;
        private const float DivisionFactor = 20f;
        private void Start()
        {
            text = PlayerInteractionHandler.SceneObjects.UI.DebugGUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Add noise on player collision
            if (collision.gameObject.CompareTag("Player"))
            {
                var noise = collision.relativeVelocity.magnitude / DivisionFactor;
                print($"{collision.relativeVelocity.magnitude} -> {collision.relativeVelocity.magnitude / DivisionFactor}");

                text.text = $"Last hit noise: {noise:##.###}";
                
                PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script.AddNoise(noise);
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    Handheld.Vibrate();
                }
            }
        }
    }
}