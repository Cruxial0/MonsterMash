using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Trigger_BelowGroundChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject plane = this.transform.root.gameObject;
        
        var localScale = plane.transform.localScale;
        print($"X: {localScale.x}, Z: {localScale.z}");
        this.transform.localScale += new Vector3(localScale.x * 2, 0, localScale.z * 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.transform.position += Vector3.up;
    }
}
