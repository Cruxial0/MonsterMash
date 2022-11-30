using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Scripts.Extensions.Enum;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

public class CrossbowTrap : MonoBehaviour, ITrapCollision
{
    public FireDirection fireDirection; // Direction for arrow fire
    public float fireIntervalSeconds = 3f; // Fire interval for arrows
    public GameObject arrowPrefab; // Arrow to fire
    public float projectileMovementMultiplier = 0.02f; // Arrow movement speed
    public float startupDelay;

    private float currStartupDelay = 0f;
    private bool _isProjectile = false; // Is object a projectile?
    private float _currTime = 0f; // Counter for fire interval
    private bool _playerHit = false; // Is player hit?
    
    public float debuffTimeSeconds = 3f; // Debuff (slow) duration
    private float _currDebuffTimer = 0f; // Current debuff timer
    private Transform player;
    
    //Dictionary for getting Vector3 values from FireDirection
    private Dictionary<FireDirection, Vector3> _fireDirection = new()
    {
        { FireDirection.Left, Vector3.left },
        { FireDirection.Up, Vector3.forward },
        { FireDirection.Right, Vector3.right },
        { FireDirection.Down, Vector3.back }
    };
    
    /// <summary>
    /// Set object as projectile
    /// </summary>
    /// <param name="self">Origin script</param>
    /// <param name="direction">Singular direction</param>
    private void EnableProjectileMode(CrossbowTrap self, FireDirection direction)
    {
        // Rotate arrow if its shot up/down
        switch (direction)
        {
            case FireDirection.Down:
                this.transform.Rotate(new Vector3(0,-90,0));
                break;
            case FireDirection.Up:
                this.transform.Rotate(new Vector3(0,90,0));
                break;
            case FireDirection.Right:
                this.transform.Rotate(new Vector3(0,180,0));
                break;
        }

        // Set direction to specified direction
        fireDirection = direction;
        projectileMovementMultiplier = self.projectileMovementMultiplier;
        debuffTimeSeconds = self.debuffTimeSeconds;
        // Is projectile.
        _isProjectile = true;
    }


    // Update is called once per frame
    void Update()
    {
        switch (_isProjectile)
        {
            // In case this object is not a projectile
            case false:

                currStartupDelay += Time.deltaTime;
                
                if(currStartupDelay < startupDelay) return;

                _currTime += Time.deltaTime; // Increment time
                if(_currTime < fireIntervalSeconds) return; // Return if interval is not complete

                // Loop through FireDirection
                foreach (var direction in fireDirection.GetFlags())
                {
                    // Instantiate new arrow prefab and apply the CrossbowTrap script for every direction
                    var c = Object.Instantiate(arrowPrefab, this.transform.position, arrowPrefab.transform.rotation)
                        .AddComponent<CrossbowTrap>();
                    // Set instantiated object as projectile
                    c.EnableProjectileMode(this, (FireDirection)direction);
                }

                _currTime = 0f; // Reset time
                break;
            
            case true:
                // Move arrow
                
                this.transform.position += _fireDirection[fireDirection] * (projectileMovementMultiplier * Time.deltaTime);
                break;
        }
        
        // If -isProjectile or _playerHit is false, return
        if(!_isProjectile || !_playerHit) return;

        player.position = this.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        // Destroy object if collided with wall
        if (other.CompareTag("Furniture") && other.name.ToLower().Contains("wall") 
            || other.CompareTag("RoomWall") && other.name.ToLower().Contains("wall"))
        {
            PlayerInteractionHandler.SceneObjects.Player.Transform.SetParent(null);

            // Make sure Update() is allowed to update before destroying
            Destroy(this.gameObject); 
        }
        
        // If collider is not player, or object is not projectile, return
        if(!other.CompareTag("Player") || !_isProjectile) return;
        
        PlayerHit(); // Inflict debuff
    }

    public string TrapName => "CrossbowTrap";
    public GameObject TrapInstance { get; set; }
    public Animation Animation { get; set; }
    public void AddInteractionHandlerReference(PlayerInteractionHandler handler) { return; }

    public void OnCollision(float playerSpeed) { return; }

    //Inflict debuff
    private void PlayerHit()
    {
        // Set movement speed to half
        //PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed *= 0.5f;

        player = PlayerInteractionHandler.SceneObjects.Player.Transform;

        _playerHit = true; // Player is hit
    }
    
    // Enum for defining fire direction
    // Enums are cool, read more about them at:
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
    [Flags]
    public enum FireDirection
    {
        Left = 1 << 1,
        Up = 1 << 2,
        Right = 1 << 3,
        Down = 1 << 4,
    }
}
