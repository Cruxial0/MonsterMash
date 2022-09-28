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
        public string powerName; //Name of associated IPower
        private IPower powerObject; //Instance of associated IPower
        // Start is called before the first frame update
        void Start()
        {
            //Get the first power from PowerManager.Powers where the PowerName = powerName
            powerObject = PlayerInteractionHandler.PowerManager.Powers.First(x => x.PowerName == powerName);
            powerObject.Parent = this.gameObject; //Assign Parent object to this.gameObject
        }

        private void OnTriggerEnter(Collider other)
        {
            //Call method IPower.Execute()
            powerObject.PowerObject.Execute();
        }
    }
}
