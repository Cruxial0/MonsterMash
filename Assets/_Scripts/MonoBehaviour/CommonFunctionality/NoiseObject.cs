using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class NoiseObject : UnityEngine.MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            //Add noise on player collision
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Script.AddNoise();
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    Handheld.Vibrate();
                }
            }
        }
    }
}