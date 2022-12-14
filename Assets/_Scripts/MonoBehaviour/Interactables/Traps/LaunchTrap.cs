using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using _Scripts.Handlers.SceneManagers.SceneObjectsHandler;
using UnityEngine;

public class LaunchTrap : MonoBehaviour, ITrapCollision
{
    public ForceDirection trapDirection;
    public ForceMode forceMode;
    public float launchForce;
    private PlayerInteractionHandler _handler;

    private SceneObjects _sceneObjects;

    private Dictionary<ForceDirection, Vector3> _launchDirection = new()
    {
        { ForceDirection.Left, Vector3.left },
        { ForceDirection.Forward, Vector3.forward },
        { ForceDirection.Up, Vector3.up },
        { ForceDirection.Right, Vector3.right },
        { ForceDirection.Down, Vector3.down }
    };

    public string TrapName => "LaunchTrap";
    public GameObject TrapInstance { get; set; }
    public Animation Animation { get; set; }
    public void AddInteractionHandlerReference(PlayerInteractionHandler handler)
    {
        _handler = handler;
        _sceneObjects = PlayerInteractionHandler.SceneObjects;
    }

    public void OnCollision(float playerSpeed)
    {
       return;
    }

    public void OnTriggerEnter(Collider other)
    {
        // Launch player
        _sceneObjects.Player.Rigidbody
            .AddForce(_launchDirection[trapDirection] * launchForce, forceMode);
        
        // Add log
        _handler.TrapHandler.Interactibles.First(x => x.Script.TrapName == TrapName)
            .AddCollisionEntry(new TrapEventArgs(this, other));
    }
    
    public enum ForceDirection
    {
        Left,
        Forward,
        Right,
        Down,
        Up,

    }
}
