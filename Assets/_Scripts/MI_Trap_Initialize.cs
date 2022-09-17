using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using UnityEngine;

public class MI_Trap_Initialize : MonoBehaviour, ITrapCollision
{
    public InteractType Type;
    private PlayerInteractionHandler _handler;
    
    // Start is called before the first frame update
    void Start()
    {
        TrapInstance = this.gameObject;
        Animation = new Animation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if(!c.gameObject.CompareTag("Player")) return;
        _handler.TrapHandler.Interactibles.First(x => x.Parent == this.gameObject)
            .AddCollisionEntry(new TrapEventArgs(this, trigger: c));
    }

    public void AddInteractionHandlerReference(PlayerInteractionHandler handler) => _handler = handler;
    
    //Inherited from ITrapCollision
    public GameObject TrapInstance { get; set; }
    public Animation Animation { get; set; }
    public void OnCollision(float playerSpeed)
    {
        _handler.GameStateManager.Lose();
    }
}