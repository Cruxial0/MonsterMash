namespace _Scripts.MonoBehaviour.CommonFunctionality
{
    /// <summary>
    /// Reusable script for destroying object
    /// </summary>
    public class DestroyThis : UnityEngine.MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            //Destroy gameObject of which this script is attached to.
            Destroy(gameObject, 1f);

            //Destroy this script to free up memory.
            Destroy(this);
        }
    }
}