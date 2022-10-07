using System;
using _Scripts.Handlers;
using UnityEngine;

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

        public void ChangeColor(int index)
        {
            PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.sprite =
                PlayerInteractionHandler.SceneObjects.Player.Sprites.Sprites[index];
            active = true;
        }

        private float currTime = 0f;
        private float maxTime = 0.2f;
        private bool active = false;
        
        private void Update()
        {
            if(!active) return;
            currTime += Time.deltaTime;

            if (currTime >= maxTime)
            {
                PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.sprite =
                    PlayerInteractionHandler.SceneObjects.Player.Sprites.Sprites[0];
                active = false;
            }
        }

        public event OnPlayerDestroyedEvent OnPlayerDestroyed;
    }
}