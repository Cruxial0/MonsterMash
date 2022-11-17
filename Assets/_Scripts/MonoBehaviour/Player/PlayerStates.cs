using System;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerStates : UnityEngine.MonoBehaviour
    {
        public PlayerState PlayerState = PlayerState.None;
        private PlayerMovmentController playerBody;

        private bool _moving = false;

        public Boolean Moving
        {
            get => _moving;
            set
            {
                if (_moving != value)
                {
                    _moving =  value;
                    var handler = PlayerMoving;
                    handler?.Invoke(value);
                    
                }
                else return;
            }
        }

        private bool _buffed = false;

        public Boolean Buffed
        {
            get => _buffed;
            set
            {
                if (_buffed != value)
                {
                    _buffed = value;
                    print(_buffed);
                    if(value) SetBuffedPlayerState();
                    if (!value)
                    {
                        print(value);
                        PlayerState &= ~PlayerState.Buffed;
                    }
                    
                    
                    var handler = PlayerBuffed;
                    handler?.Invoke(value);
                }
            }
        }
        
        [NonSerialized] public bool Destroyed = false;

        private void Start()
        {
            playerBody = PlayerInteractionHandler.SceneObjects.Player.MovmentController;
        }

        //Invokes OnPlayerDestroyed Event
        private void OnDestroy()
        {
            var handler = OnPlayerDestroyed;
            handler?.Invoke(true);
        }

        //Destroys parent
        public void DestroySelf()
        {
            if(this == null) return;
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
            
            if (playerBody.acceleration >= 1) OnPlayerMoving();
            else
            {
                PlayerState &= ~PlayerState.Moving;
                Moving = false;
            }
            
            if (PlayerState == 0) PlayerState = PlayerState.None;
            
            if(!active) return;
            currTime += Time.deltaTime;

            if (currTime >= maxTime)
            {
                PlayerInteractionHandler.SceneObjects.Player.Sprite.SpriteRenderer.sprite =
                    PlayerInteractionHandler.SceneObjects.Player.Sprites.Sprites[0];
                active = false;
            }
        }

        public void SetBuffed(bool b)
        {
            Buffed = b;
        }

        public event OnPlayerDestroyedEvent OnPlayerDestroyed;
        public delegate void OnPlayerDestroyedEvent(bool destroyed);

        #region PlayerMovingEvent

        public event PlayerMovingEvent PlayerMoving;
        public delegate void PlayerMovingEvent(bool moving);

        private void OnPlayerMoving()
        {
            Moving = true;
            
            if(PlayerState == PlayerState.None)
                PlayerState = PlayerState.Moving;
            else
                PlayerState |= PlayerState.Moving;
        }

        #endregion


        #region PlayerBuffed

        public event PlayerBuffedEvent PlayerBuffed;
        public delegate void PlayerBuffedEvent(bool buffed);

        public void SetBuffedPlayerState()
        {
            if(PlayerState == PlayerState.None)
                PlayerState = PlayerState.Buffed;
            else
                PlayerState |= PlayerState.Buffed;
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