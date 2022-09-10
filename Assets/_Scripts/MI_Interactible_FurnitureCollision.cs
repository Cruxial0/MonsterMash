using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_FurnitureCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Play SFX
                //Play animation
                
            }
        }
    }
}
