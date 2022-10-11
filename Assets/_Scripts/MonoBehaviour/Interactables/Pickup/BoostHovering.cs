using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostHovering : MonoBehaviour
{

    public float amp;
    public float freq;
    Vector3 initPosit;
    // Start is called before the first frame update
    void Start()
    {
        initPosit = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initPosit.x, 0.5f + Mathf.Sin(Time.time * freq) * amp, initPosit.z);
        
    }
}
