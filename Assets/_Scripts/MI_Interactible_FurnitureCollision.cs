using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_FurnitureCollision : MonoBehaviour
    {
        //This is a flag. This flag essentially makes visualFeedback public.
        [SerializeField]
        private GameObject visualFeedback;
        
        //OnCollisionEnter is called when something collides with Collider and/or Rigidbody
        private void OnCollisionEnter(Collision collision)
        {
            //if colliding object has the "Player" tag, proceed
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO:
                //Play SFX
                //Play animation
                //Properly position origin point of visualFeedback (using Mesh.bounds?)

                Vector3 instantiatePos = this.gameObject.transform.GetChild(0).position;
                instantiatePos.y = 0;

                if (collision.gameObject.transform.position.z < instantiatePos.z)
                    instantiatePos.z -= gameObject.transform.localScale.z / 0.5f;
                else
                    instantiatePos.z += gameObject.transform.localScale.z / 0.5f;

                    //Create instance of visualFeedback (probably some kind of animation or particle system)
                //Also create it with it's original rotation, and on our gameObject's position.
                Instantiate(visualFeedback, instantiatePos, visualFeedback.transform.rotation);

            }
        }
    }
}
