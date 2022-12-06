using System.Collections;
using System.Collections.Generic;
using _Scripts.GUI.MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUnlockToggle : MonoBehaviour
{
    private Toggle _toggle;
    // Start is called before the first frame update
    void Start()
    {
        _toggle = this.GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ValueChanged);
    }

    // Unlock levels when value was changed
    private static void ValueChanged(bool v) => LevelList.UnlockAllLevels(v);
}
