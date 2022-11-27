using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{


    public float xCordinate = -2f;
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(Player == null)
        {
            Player = null;
        }
        else
        {
            transform.position = Player.transform.position + new Vector3(0, 10, xCordinate);
        }
        





    }
}
