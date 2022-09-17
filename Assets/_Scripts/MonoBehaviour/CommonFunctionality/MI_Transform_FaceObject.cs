using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Transform_FaceObject : MonoBehaviour
{
    public GameObject ObjectToFace;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(ObjectToFace.transform);
    }
}
