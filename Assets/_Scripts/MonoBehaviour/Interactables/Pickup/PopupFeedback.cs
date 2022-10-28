using System;
using UnityEngine;

namespace _Scripts.MonoBehaviour.Interactables.Pickup
{
    public class PopupFeedback : UnityEngine.MonoBehaviour
    {
        private void Start()
        {
            this.GetComponent<Animation>().Play("PickupAnimation");
            Destroy(this.gameObject, 10f);
        }
    }
}