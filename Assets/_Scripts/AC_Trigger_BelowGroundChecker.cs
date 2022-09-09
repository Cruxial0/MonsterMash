using UnityEngine;

namespace _Scripts
{
    public class AcTriggerBelowGroundChecker : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            //Get plane from root.
            GameObject plane = this.transform.root.gameObject;
        
            //Get the plane's scale
            var localScale = plane.transform.localScale;
            
            //Use simple mathematic formula to dynamically scale the collider to the size of the ground.
            this.transform.localScale += new Vector3(localScale.x * 2, 0, localScale.z * 2);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        //OnCollisionEnter is called when the Collider component detects a collision
        private void OnCollisionEnter(Collision collision)
        {
            //Get colliding object and shift its Y-axis by +1 (up by 1)
            collision.collider.gameObject.transform.position += Vector3.up;
        }
    }
}
