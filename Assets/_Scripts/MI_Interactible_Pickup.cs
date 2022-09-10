using System;
using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_Pickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                print($"{collision.gameObject.name} entered");
                Destroy(this.gameObject);
                Destroy(this);
            }
        }
    }
}
