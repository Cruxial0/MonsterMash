using System;
using _Scripts.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class AnimScript : MonoBehaviour
{

    public ParticleSystem deathParticle;
    [NonSerialized] public Animator Anim;

    Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = PlayerInteractionHandler.SceneObjects.Player.MovmentController.Joystick.Direction;

        var collided = PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerState;

        PlayerInteractionHandler.Self.OnCollided += Self_OnCollided;

        if (playerVelocity != Vector2.zero)
            Anim.SetBool("Run", true);
        else      
            Anim.SetBool("Run", false);
    }

    private void Self_OnCollided()
    {
        Anim.SetTrigger("bump");   
    }

    public void deathAnim()
    {
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        
        Anim.SetTrigger("Death");
    }


    public void BearTrapAnim()
    {
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        Anim.SetTrigger("BearTrap");       
    }


    public void EatAnim()
    {
        Anim.SetTrigger("Eat");
    }
}
