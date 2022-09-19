using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class NoiseObject : UnityEngine.MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeter.Script.AddNoise();
        }
    }
}
