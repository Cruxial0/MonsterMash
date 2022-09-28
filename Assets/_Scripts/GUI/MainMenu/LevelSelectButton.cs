using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.GUI.MainMenu
{
    public class LevelSelectButton
    {
        public UnityAction ToLevelList(GameObject parent, GameObject target)
        {
            parent.SetActive(false); //Deactivate parent canvas
            target.SetActive(true); //Activate target canvas
            return null;
        }
    }
}
