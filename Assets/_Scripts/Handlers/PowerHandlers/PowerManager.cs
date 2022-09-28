using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;

namespace _Scripts.Handlers.Powers
{
    public class PowerManager
    {
        //List of all IPower objects
        public List<IPower> Powers = new();

        public void GetAll()
        {
            var type = typeof(IPower); //type is type of IPower
            //Get all instances of type in the project
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToArray();

            foreach (var power in types)
            {
                if (!power.IsClass) continue; //if power is not class, continue
                Powers.Add((IPower)Activator.CreateInstance(power)); //Add instance of power to Powers
            }
        }
    }
}