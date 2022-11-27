using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.MonoBehaviour.Powers
{
    public class PowerInitialize : UnityEngine.MonoBehaviour
    {
        public string powerName; //Name of associated IPower
        public float powerDuration = 3f;

        private IPower powerObject; //Instance of associated IPower

        // Start is called before the first frame update
        private void Start()
        {
            //Get the first power from PowerManager.Powers where the PowerName = powerName
            var type = PlayerInteractionHandler.PowerManager.Powers.First(x => x.PowerName == powerName).GetType();

            powerObject = Activator.CreateInstance(type) as IPower;
            print(powerObject.GetType());
            
            powerObject.Parent = gameObject; //Assign Parent object to this.gameObject
            powerObject.PowerDuration = powerDuration;
        
        }

        private void OnTriggerEnter(Collider other)
        {
            //Call method IPower.Execute()
            powerObject.PowerObject.Execute();
        }
    }
}