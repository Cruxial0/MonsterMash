using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerStates : UnityEngine.MonoBehaviour
    {
        [NonSerialized]
        public bool Destroyed = false;

        public void DestroySelf() => Destroy(this.gameObject);
        private void OnDestroy() => OnPlayerDestroyed?.Invoke(true);

        public event OnPlayerDestroyedEvent OnPlayerDestroyed;
        public delegate void OnPlayerDestroyedEvent(bool destroyed);
    }
}