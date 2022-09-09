using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Camera_PositionManager : MonoBehaviour
{
    public GameObject player;

    public bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnabled) return;
        
        Vector3 playerPos = new Vector3();
        var position = player.transform.position;
        playerPos.x = position.x;
        playerPos.z = position.z;
        playerPos.y = position.y + 14f;

        this.transform.position = playerPos;
    }
}
