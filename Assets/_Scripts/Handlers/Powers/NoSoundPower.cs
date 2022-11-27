using System;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Handlers.Powers
{
    public class NoSoundPower : UnityEngine.MonoBehaviour, IPower
    {
        private RuntimeAnimatorController runtimeAnimatorController;
        private SpriteRenderer spriteRenderer;
        private Color defaultColor;

        public SceneObjects SceneObjects = PlayerInteractionHandler.SceneObjects;
        public string PowerName => "MutePower";
        public float PowerDuration { get; set; }
        public string PowerDescription => "Prevents noise gain for a period of time.";
        public PowerObject PowerObject => new(MuteLogic);
        public GameObject Parent { get; set; }

        public void MuteLogic()
        {
            var c = Parent.AddComponent<NoSoundPower>(); //Add component of power
            c.PowerDuration = this.PowerDuration;
            
            //Disable power visuals
            Parent.GetComponent<Renderer>().enabled = false;
            Parent.GetComponent<Collider>().enabled = false;
        }

        private bool _enabled = false;
        private float _buffTime = 5f;
        private float _currTime = 0f;

        private void Start()
        {
            Parent = this.gameObject;
            
            SceneObjects.UI.NoiseMeterSceneObject.Script.IsMute = true;
            
            spriteRenderer = SceneObjects.Player.Sprite.Plane.GetComponent<SpriteRenderer>();
            defaultColor = spriteRenderer.color;
            spriteRenderer.color = new Color(124, 124, 255);
            runtimeAnimatorController = PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController;
            PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController = null;

            
            _enabled = true;
        }

        private void Update()
        {
            if(!_enabled) return;

            _currTime += Time.deltaTime;
            
            if(_currTime < PowerDuration) return;
            
            PlayerInteractionHandler.SceneObjects.UI.NoiseMeterSceneObject.Script.IsMute = false;
            spriteRenderer.color = defaultColor;
            PlayerInteractionHandler.SceneObjects.Player.AnimScript.Anim.runtimeAnimatorController =
                runtimeAnimatorController;
            
            Destroy(this);
        }
    }
}