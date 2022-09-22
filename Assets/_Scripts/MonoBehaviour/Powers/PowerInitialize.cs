using System;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.Handlers;
using _Scripts.Handlers.PowerHandlers;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.MonoBehaviour.Powers
{
    public class PowerInitialize : UnityEngine.MonoBehaviour
    {
        public string powerName;
        private IPower powerObject;
        // Start is called before the first frame update
        void Start()
        {
            powerObject = PlayerInteractionHandler.PowerManager.Powers.First(x => x.PowerName == powerName);
            powerObject.Parent = this.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            powerObject.PowerObject.Execute();
        }
    }
}
