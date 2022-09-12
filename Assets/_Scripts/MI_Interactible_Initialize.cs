using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using UnityEngine;

public class MI_Interactible_Initialize : MonoBehaviour
{
    public InteractType Type;
    public ParticleSystem VisualFeedback;
    private PlayerInteractionHandler _handler;
    void Awake()
    {
        if(Type == null) Destroy(this.gameObject);
    }

    public void AddInteractionHandlerReference(PlayerInteractionHandler handler) => _handler = handler;

    private void OnCollisionEnter(Collision collision)
    {
        _handler.InteractableHandler.Interactibles.First(x => x.Parent == this.gameObject)
            .AddCollisionEntry(collision);
    }
}
