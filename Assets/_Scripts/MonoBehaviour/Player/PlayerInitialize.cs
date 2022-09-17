using _Scripts.Handlers;

namespace _Scripts.MonoBehaviour.Player
{
    public class PlayerInitialize : UnityEngine.MonoBehaviour
    {
        public static PlayerInteractionHandler PlayerInteractionHandler;
        void Start()
        {
            PlayerInteractionHandler = new PlayerInteractionHandler(this.gameObject);
        }
    }
}
