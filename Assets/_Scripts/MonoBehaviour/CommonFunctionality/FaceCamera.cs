using UnityEngine;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class FaceCamera : UnityEngine.MonoBehaviour
    {
        public GameObject ObjectToFace;
        // Update is called once per frame
        void Update()
        {
            //Looks at ObjectToFace
            this.gameObject.transform.LookAt(ObjectToFace.transform);
        }
    }
}
