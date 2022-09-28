using System;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerStates : UnityEngine.MonoBehaviour
    {
        public delegate void OnPlayerDestroyedEvent(bool destroyed);

        [NonSerialized] public bool Destroyed = false;

        //Invokes OnPlayerDestroyed Event
        private void OnDestroy()
        {
            OnPlayerDestroyed?.Invoke(true);
        }

        //Destroys parent
        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public event OnPlayerDestroyedEvent OnPlayerDestroyed;
    }
}