using _Scripts.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{



    public ParticleSystem deathParticle;
    Animator anim;

    Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        playerVelocity = PlayerInteractionHandler.SceneObjects.Player.MovmentController.Joystick.Direction;

        var collided = PlayerInteractionHandler.SceneObjects.Player.PlayerStates.PlayerState;

        PlayerInteractionHandler.Self.OnCollided += Self_OnCollided;

        if (playerVelocity != Vector2.zero)
            anim.SetBool("Run", true);
        else      
            anim.SetBool("Run", false);



        

    }

    private void Self_OnCollided()
    {
        anim.SetTrigger("bump");   
    }

    public void deathAnim()
    {
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        
        anim.SetTrigger("Death");
    }


    public void BearTrapAnim()
    {
        Object.Instantiate(deathParticle).transform.position = PlayerInteractionHandler.SceneObjects.Player.Transform.position;
        anim.SetTrigger("BearTrap");       
    }


    public void EatAnim()
    {
        anim.SetTrigger("Eat");
    }
}
