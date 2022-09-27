using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

namespace _Scripts.Handlers.Interfaces
{
    public interface ITrapCollision
    {
        public string TrapName { get; }
        public GameObject TrapInstance { get; set; }
        public Animation Animation { get; set; }
        public void AddInteractionHandlerReference(PlayerInteractionHandler handler);
        public void OnCollision(float playerSpeed);
    }
}