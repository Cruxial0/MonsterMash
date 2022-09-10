using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Opponent_MovementSync : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _opponentClone;
    void Start()
    {
        GameObject[] thingyToFind = GameObject.FindGameObjectsWithTag("Opponent");

        print(thingyToFind.Length);
        if (thingyToFind == null || thingyToFind.Length != 1)
        {
            Destroy(this);
            return;
        }
        _opponentClone = Instantiate(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 opponentOriginPos = this.gameObject.transform.position;
        Vector3 opponentOriginRot = this.gameObject.transform.eulerAngles;

        opponentOriginPos.x -= 90f;

        RaycastHit hit;
        if (Physics.Raycast(_opponentClone.transform.position, transform.TransformDirection(Vector3.down), out hit,
                Mathf.Infinity))
        {
            if (hit.distance > 0.3f)
            {
                print(hit.collider.gameObject.name);
                opponentOriginPos.y -= hit.distance;
            }
        }

        if(_opponentClone == null) Destroy(this);
        _opponentClone.transform.position = opponentOriginPos;
    }
}
