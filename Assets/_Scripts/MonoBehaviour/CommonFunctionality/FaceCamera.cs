using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    /// <summary>
    /// Experimental script for having objects face the camera
    /// </summary>
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