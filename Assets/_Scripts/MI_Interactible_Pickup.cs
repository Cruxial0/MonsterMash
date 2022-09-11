using System;
using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_Pickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            //If collider has the "Player" tag, proceed.
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO:
                //Add points to external handler
                //Add SFX and VFX
                
                //Destroy script-holder.
                Destroy(this.gameObject);
                //Destroy this script instance to free up memory.
                Destroy(this);
            }
        }
    }
}
