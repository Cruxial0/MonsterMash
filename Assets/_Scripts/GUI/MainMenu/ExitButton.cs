using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Button;
    private void Start()
    {
        Button.onClick.AddListener(ExitGame);
    }

    private void ExitGame() => Application.Quit();
}
