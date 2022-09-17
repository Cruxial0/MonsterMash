using UnityEngine;

namespace _Scripts.Handlers.Interfaces
{
    public interface ITrapCollision
    {
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }
        public void OnCollision(float playerSpeed);
    }
}