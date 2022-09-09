using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
            var rotationX = transform.rotation.x;
            var rotationZ = transform.rotation.z;
            var rotationY = transform.rotation.y;
            if (_keyboard.wKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(rotationRate, 0f, 0f);
                    if (rotationX < 0) rotation.x += rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }

            if (_keyboard.sKey.IsPressed())
            {
                if (rotationX is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(-rotationRate, 0f, 0f);
                    if (rotationX > 0) rotation.x -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            if (_keyboard.aKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, rotationRate);
                    if (rotationZ < 0) rotation.z += rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }
        
            if (_keyboard.dKey.IsPressed())
            {
                if (rotationZ is < MaxRotation and > -MaxRotation)
                {
                    var rotation = new Vector3(0f, 0f, -rotationRate);
                    if (rotationZ > 0) rotation.z -= rotationRate;
                    this.transform.Rotate(rotation  * Time.deltaTime);
                    return;
                }
            }

            switch (rotationX)
            {
                case 0 when rotationZ == 0:
                    return;
                case > 0:
                    this.transform.Rotate(-rotationRate  * Time.deltaTime, 0, 0);
                    break;
                case < 0:
                    this.transform.Rotate(rotationRate  * Time.deltaTime, 0, 0);
                    break;
            }

            switch (rotationZ)
            {
                case < 0:
                    this.transform.Rotate(0, 0, rotationRate  * Time.deltaTime);
                    break;
                case > 0:
                    this.transform.Rotate(0, 0, -rotationRate  * Time.deltaTime);
                    break;
            }

            switch (rotationY)
            {
                case > 0:
                    this.transform.Rotate(0, -rotationRate  * Time.deltaTime, 0);
                    break;
                case < 0:
                    this.transform.Rotate(0, rotationRate  * Time.deltaTime, 0);
                    break;
            }
        }
    }
}
