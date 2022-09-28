using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerStates : UnityEngine.MonoBehaviour
    {
        [NonSerialized]
        public bool Destroyed = false;

        //Destroys parent
        public void DestroySelf() => Destroy(this.gameObject);
        
        //Invokes OnPlayerDestroyed Event
        private void OnDestroy() => OnPlayerDestroyed?.Invoke(true);

        public event OnPlayerDestroyedEvent OnPlayerDestroyed;
        public delegate void OnPlayerDestroyedEvent(bool destroyed);
    }
}