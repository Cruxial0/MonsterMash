using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    public class HideSelected : UnityEngine.MonoBehaviour
    {
        public GameObject objectToHide;

        public GameObject objectToShow;
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(Hide);
        }

        void Hide()
        {
            objectToHide.SetActive(false);
            objectToShow.SetActive(true);
        }
    }
}
