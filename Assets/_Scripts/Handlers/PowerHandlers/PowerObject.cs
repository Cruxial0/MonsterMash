using UnityEngine.Events;

namespace _Scripts.Handlers.PowerHandlers
{
    public class PowerObject
    {
        public UnityAction PowerLogic; //Associated script

        //Set PowerLogic to action
        public PowerObject(UnityAction action) => PowerLogic = action;

        public void Execute() => PowerLogic.Invoke(); //Execute script
    }
}