using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using _Scripts.Extensions.Enum;
using _Scripts.Handlers;
using _Scripts.Handlers.Interfaces;
using cakeslice;
using UnityEngine;
using Object = UnityEngine.Object;

public class CrossbowTrap : MonoBehaviour, ITrapCollision
{
    public FireDirection fireDirection; // Direction for arrow fire
    public float fireIntervalSeconds = 3f; // Fire interval for arrows
    public GameObject arrowPrefab; // Arrow to fire
    public float projectileMovementMultiplier = 0.02f; // Arrow movement speed
    
    private bool _isProjectile = false; // Is object a projectile?
    private float _currTime = 0f; // Counter for fire interval
    private bool _playerHit = false; // Is player hit?
    
    public float debuffTimeSeconds = 3f; // Debuff (slow) duration
    private float _currDebuffTimer = 0f; // Current debuff timer
    
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
    private void EnableProjectileMode(CrossbowTrap self, FireDirection direction = FireDirection.None)
    {
        // Rotate arrow if its shot up/down
        if(direction == FireDirection.Down || direction == FireDirection.Up)
            this.transform.Rotate(new Vector3(0,90,0));
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
                this.transform.position += _fireDirection[fireDirection] * projectileMovementMultiplier;
                break;
        }
        
        // If -isProjectile or _playerHit is false, return
        if(!_isProjectile || !_playerHit) return;
        
        // Increment time
        _currDebuffTimer += Time.deltaTime;
        
        // If interval didn't elapse, return
        if (_currDebuffTimer < debuffTimeSeconds) return;

        // Revert movement speed
        PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed =
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.DefaultMovementSpeed;
        
        _currDebuffTimer = 0f; // Reset time
        _playerHit = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // Destroy object if collided with wall
        if (other.CompareTag("RoomWall"))
        {
            this.GetComponent<Renderer>().enabled = false; // Fake destroy
            Destroy(this.GetComponent<Outline>());
            
            // Make sure Update() is allowed to update before destroying
            Destroy(this.gameObject, debuffTimeSeconds); 
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
        PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed *= 0.5f;
        _playerHit = true; // Player is hit
    }
    
    // Enum for defining fire direction
    // Enums are cool, read more about them at:
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
    [Flags]
    public enum FireDirection
    {
        None = 1 << 0,
        Left = 1 << 1,
        Up = 1 << 2,
        Right = 1 << 3,
        Down = 1 << 4,
    }
}
