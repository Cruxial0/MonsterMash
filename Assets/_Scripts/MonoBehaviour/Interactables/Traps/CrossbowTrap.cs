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
    public FireDirection fireDirection;
    public float fireIntervalSeconds = 3f;
    public GameObject arrowPrefab;
    public float projectileMovementMultiplier = 0.02f;
    private bool _isProjectile = false;
    private float _currTime = 0f;
    private bool _playerHit = false;
    public float debuffTimeSeconds = 3f;
    private float _currDebuffTimer = 0f;
    
    private Dictionary<FireDirection, Vector3> _fireDirection = new()
    {
        { FireDirection.Left, Vector3.left },
        { FireDirection.Up, Vector3.forward },
        { FireDirection.Right, Vector3.right },
        { FireDirection.Down, Vector3.back }
    };
    
    public void EnableProjectileMode(CrossbowTrap self, FireDirection direction = FireDirection.None)
    {
        if(direction == FireDirection.Down || direction == FireDirection.Up)
            this.transform.Rotate(new Vector3(0,90,0));
        fireDirection = direction;
        _isProjectile = true;
    }


    // Update is called once per frame
    void Update()
    {
        switch (_isProjectile)
        {
            case false:
                _currTime += Time.deltaTime;
                if(_currTime < fireIntervalSeconds) return;

                int i = 0;
                foreach (var direction in fireDirection.GetFlags())
                {
                    i++;
                    
                    var c = Object.Instantiate(arrowPrefab, this.transform.position, arrowPrefab.transform.rotation)
                        .AddComponent<CrossbowTrap>();
                    c.EnableProjectileMode(this, (FireDirection)direction);
                }

                _currTime = 0f;
                break;
            
            case true:
                this.transform.position += _fireDirection[fireDirection] * projectileMovementMultiplier;
                break;
        }
        
        if(!_isProjectile || !_playerHit) return;

        print(_currDebuffTimer);
        _currDebuffTimer += Time.deltaTime;
        
        if (_currDebuffTimer < debuffTimeSeconds) return;

        PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed =
            PlayerInteractionHandler.SceneObjects.Player.MovmentController.DefaultMovementSpeed;
        
        _currDebuffTimer = 0f;
        _playerHit = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomWall"))
        {
            this.GetComponent<Renderer>().enabled = false;
            Destroy(this.gameObject, debuffTimeSeconds);
        }
        
        if(!other.CompareTag("Player")) return;
        
        PlayerHit();
    }

    [Flags]
    public enum FireDirection
    {
        None = 1 << 0,
        Left = 1 << 1,
        Up = 1 << 2,
        Right = 1 << 3,
        Down = 1 << 4,
    }

    public string TrapName { get; }
    public GameObject TrapInstance { get; set; }
    public Animation Animation { get; set; }
    public void AddInteractionHandlerReference(PlayerInteractionHandler handler) { return; }

    public void OnCollision(float playerSpeed) { return; }

    private void PlayerHit()
    {
        PlayerInteractionHandler.SceneObjects.Player.MovmentController.MovementSpeed *= 0.5f;
        _playerHit = true;
    }
}
