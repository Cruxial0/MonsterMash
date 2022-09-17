using UnityEngine;

namespace _Scripts.Handlers
{
    public class GameStateManager
    {
        private GameObject _player;
        private PlayerInteractionHandler _handler;
        public GameStateManager(GameObject player, PlayerInteractionHandler handler)
        {
            _player = player;
            _handler = handler;
        }

        public void Lose()
        {
            Object.Destroy(_player);
            Debug.Log("You lose");
        }
        
        public void Win()
        {
            Object.Destroy(_player);
            Debug.Log("You win");
        }
    }
}