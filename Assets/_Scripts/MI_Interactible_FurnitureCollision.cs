using UnityEngine;

namespace _Scripts
{
    public class MI_Interactible_FurnitureCollision : MonoBehaviour
    {
        [SerializeField]
        GameObject visualFeedback;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //TODO:
                //Play SFX
                //Play animation

                print("collided");
                var obj = Instantiate(visualFeedback, this.gameObject.transform.position, visualFeedback.transform.rotation);
                obj.transform.GetChild(0).transform.position = transform.position;
                //obj.transform.GetChild(0).transform.SetParent(this.transform, false);
            }
        }
    }
}
