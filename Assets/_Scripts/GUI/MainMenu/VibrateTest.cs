using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateTest : MonoBehaviour
{
    public Button b;
    // Start is called before the first frame update
    void Start()
    {
        b.onClick.AddListener(Vibrate);
    }

    void Vibrate()
    {
        
    }
}
