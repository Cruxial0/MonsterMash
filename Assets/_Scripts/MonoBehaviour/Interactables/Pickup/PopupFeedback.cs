using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Pickup
{
    public class PopupFeedback : UnityEngine.MonoBehaviour
    {
        private Quaternion iniRotation;
        private void Start()
        { 
            iniRotation = this.transform.rotation;
            this.GetComponent<Animation>().Play("PickupAnimation");
            Destroy(this.gameObject, 10f);
        }

        private void LateUpdate()
        {
            this.transform.rotation = iniRotation;
        }
    }
}