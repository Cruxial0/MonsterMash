using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialRandomize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mats = GetComponent<MaterialContainer>().materials;
        this.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Count)];
    }
}
