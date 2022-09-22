using UnityEngine.Events;

namespace _Scripts.Handlers.PowerHandlers
{
    public class PowerObject
    {
        public UnityAction PowerLogic;
        
        public PowerObject(UnityAction action)
        {
            PowerLogic = action;
        }

        public void Execute() => PowerLogic.Invoke();
    }
}