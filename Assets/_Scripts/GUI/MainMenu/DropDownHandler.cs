using System;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.MonoBehaviour.Player;
using TMPro;
using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
    public static PlayerMovmentController.ControlType ControlType;
    public TMP_Dropdown dropdownMenu;
    // Start is called before the first frame update
    void Start()
    {
        //Add OnValueChanged method as listener
        dropdownMenu.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(int i)
    {
        //Format string to match Enum types, then parse said string into an Enum
        //Learn about Enums here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
        PlayerMovmentController.ControlType type = (PlayerMovmentController.ControlType)Enum.Parse(
            typeof(PlayerMovmentController.ControlType),
            dropdownMenu.options[i].text.Split(" ").First(), true);
        ControlType = type;
    }
}
