using _Scripts.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{

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

        if (collided == _Scripts.MonoBehaviour.Player.PlayerState.Collided)
        {
            anim.SetTrigger("bump");

        }



        if (playerVelocity != Vector2.zero)
        {
            anim.SetBool("Run", true);
            Debug.Log("IS WORK");

        }
        else
        {
            anim.SetBool("Run", false);
            Debug.Log("NO MORE");
        }
      
       

    }
}
