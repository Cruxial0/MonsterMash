using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class FaceCamera : UnityEngine.MonoBehaviour
    {
        public GameObject ObjectToFace;

        // Update is called once per frame
        private void Update()
        {
            //Looks at ObjectToFace
            gameObject.transform.LookAt(ObjectToFace.transform);
        }
    }
}