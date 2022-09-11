using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI_Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Remove), 1);
    }

    void Remove()
    {
        Destroy(gameObject, 1f);
    }
}
