using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomScript : MonoBehaviour
{
    public Material[] spriteMaterials;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        

        rend.material = spriteMaterials[Random.Range(0, spriteMaterials.Length)];
        
        


    }


}
