using System;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerStates : UnityEngine.MonoBehaviour
    {
        public PlayerState PlayerState = PlayerState.None;
        private Rigidbody playerBody; 

        [NonSerialized] public bool Destroyed = false;

        private void Start()
        {
            playerBody = PlayerInteractionHandler.SceneObjects.Player.Rigidbody;
        }

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
            if(playerBody.velocity.magnitude != 0) OnPlayerMoving();
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
        public delegate void OnPlayerDestroyedEvent(bool destroyed);

        #region PlayerMovingEvent

        public event PlayerMovingEvent PlayerMoving;
        public delegate void PlayerMovingEvent();

        protected virtual void OnPlayerMoving()
        {
            PlayerState |= PlayerState.Moving;
            PlayerMoving?.Invoke();
        }

        #endregion
        
    }

    [Flags]
    public enum PlayerState
    {
        None = 1 << 0,
        Dead = 1 << 1,
        Collided = 1 << 2,
        Moving = 1 << 3,
        Buffed = 1 << 4
    }
}