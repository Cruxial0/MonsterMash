using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    public class AC_Ground_RotationController : MonoBehaviour
    {
        public GameObject player;
        public float rotationRate;
        private const float MaxRotation = 0.30f;
        private Keyboard _keyboard;
        // Start is called before the first frame update
        void Start()
        {
            _keyboard = Keyboard.current;
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (_keyboard.wKey.IsPressed())
            {
                if (transform.rotation.x is < MaxRotation and > -MaxRotation)
                {
                    this.transform.Rotate(new Vector3(rotationRate, 0, 0)  * Time.deltaTime);
                    return;
                }
            }

            if (_keyboard.sKey.IsPressed())
            {
                if (transform.rotation.x is < MaxRotation and > -MaxRotation)
                {
                    this.transform.Rotate(new Vector3(-rotationRate, 0, 0)  * Time.deltaTime);
                    return;
                }
            }
        
            if (_keyboard.aKey.IsPressed())
            {
                if (transform.rotation.z is < MaxRotation and > -MaxRotation)
                {
                    this.transform.Rotate(new Vector3(0, 0, rotationRate)  * Time.deltaTime);
                    return;
                }
            }
        
            if (_keyboard.dKey.IsPressed())
            {
                if (transform.rotation.z is < MaxRotation and > -MaxRotation)
                {
                    this.transform.Rotate(new Vector3(0, 0, -rotationRate)  * Time.deltaTime);
                    return;
                }
            }
        
            if(transform.rotation.x == 0 && transform.rotation.z == 0) return;
        
            if(transform.rotation.x > 0) 
                this.transform.Rotate(-rotationRate  * Time.deltaTime, 0, 0);
            if(transform.rotation.x < 0) 
                this.transform.Rotate(rotationRate  * Time.deltaTime, 0, 0);
            if(transform.rotation.z < 0) 
                this.transform.Rotate(0, 0, rotationRate  * Time.deltaTime);
            if(transform.rotation.z > 0) 
                this.transform.Rotate(0, 0, -rotationRate  * Time.deltaTime);
            if(transform.rotation.y > 0) 
                this.transform.Rotate(0, -rotationRate  * Time.deltaTime, 0);
            if(transform.rotation.y < 0) 
                this.transform.Rotate(0, rotationRate  * Time.deltaTime, 0);
        }
    }
}
