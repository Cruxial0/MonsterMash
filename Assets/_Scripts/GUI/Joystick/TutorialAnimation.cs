using UnityEngine;

namespace _Scripts.GUI.Joystick
{
    public class TutorialAnimation : UnityEngine.MonoBehaviour
    {
        // Start is called before the first frame update
        void Start() 
            => this.GetComponent<Animator>().Play("Joystick Animation");
    }
}
