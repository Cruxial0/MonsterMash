using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTest : MonoBehaviour
{
    /// <summary>
    /// Script written by Henrik for debugging purposes
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Furniture")
        {

            GetComponent<Animator>().SetTrigger("Death");
            Debug.LogError("YES");

        }
    }
}
