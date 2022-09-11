using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_FurnitureCollision : MonoBehaviour
    {
        //This is a flag. This flag essentially makes visualFeedback public.
        [SerializeField]
        GameObject visualFeedback;
        
        //OnCollisionEnter is called when something collides with Collider and/or Rigidbody
        private void OnCollisionEnter(Collision collision)
        {
            //if colliding object has the "Player" tag, proceed
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO:
                //Play SFX
                //Play animation
                
                //Create instance of visualFeedback (probably some kind of animation or particle system)
                //Also create it with it's original rotation, and on our gameObject's position.
                var obj = Instantiate(visualFeedback, this.gameObject.transform.position, visualFeedback.transform.rotation);

            }
        }
    }
}
