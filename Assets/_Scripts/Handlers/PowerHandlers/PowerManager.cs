using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Interfaces;

namespace _Scripts.Handlers.Powers
{
    public class PowerManager
    {
        public List<IPower> Powers = new List<IPower>();
        public void GetAll()
        {
            var type = typeof(IPower);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .ToArray();
            
            foreach (var power in types)
            {
                if (!power.IsClass) continue;
                Powers.Add((IPower)Activator.CreateInstance(power));
            }
        }
    }
}