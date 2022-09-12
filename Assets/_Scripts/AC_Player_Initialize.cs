using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Handlers;
using UnityEngine;

public class AC_Player_Initialize : MonoBehaviour
{
    public static PlayerInteractionHandler PlayerInteractionHandler;
    void Start()
    {
        PlayerInteractionHandler = new PlayerInteractionHandler(this.gameObject);
        print(PlayerInteractionHandler.InteractableHandler.Interactibles.Count);
    }
}
