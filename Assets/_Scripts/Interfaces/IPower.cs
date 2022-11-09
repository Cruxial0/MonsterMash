using _Scripts.Handlers.PowerHandlers;
using UnityEngine;

namespace _Scripts.Interfaces
{
    public interface IPower
    {
        public string PowerName { get; }
        public float PowerDuration { get; set; }
        public string PowerDescription { get; }
        public PowerObject PowerObject { get; }
        public GameObject Parent { get; set; }
    }
}