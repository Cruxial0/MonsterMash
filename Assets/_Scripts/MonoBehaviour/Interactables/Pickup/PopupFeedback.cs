using System;
using _Scripts.Handlers;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.MonoBehaviour.Interactables.Pickup
{
    public class PopupFeedback : UnityEngine.MonoBehaviour
    {
        [NonSerialized] public bool LastPickup = false;
        private SceneObjects _objects = PlayerInteractionHandler.SceneObjects;
        private Quaternion _iniRotation;
        private TextMeshPro _textMesh;
        private float _currTime = 0f;
        private float _cycleTime = 0.5f;
        private void Start()
        {
            _textMesh = this.GetComponent<TextMeshPro>();
            _textMesh.color = Color.HSVToRGB(.34f, .84f, .67f);
            _iniRotation = this.transform.rotation;
            this.GetComponent<Animation>().Play("PickupAnimation");
            Destroy(this.gameObject, 10f);
        }

        private void Update()
        {
            if(!LastPickup) return;
            
            // Assign HSV values to float h, s & v. (Since material.color is stored in RGB)
            float h, s, v;
            Color.RGBToHSV(_textMesh.color, out h, out s, out v);
 
            // Use HSV values to increase H in HSVToRGB. It looks like putting a value greater than 1 will round % 1 it
            _textMesh.color = Color.HSVToRGB(h + Time.deltaTime * .65f, s, v);
        }

        private void LateUpdate()
        {
            this.transform.rotation = _iniRotation;
        }
    }
}