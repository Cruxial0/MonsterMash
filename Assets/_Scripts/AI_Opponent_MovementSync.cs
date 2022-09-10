using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Opponent_MovementSync : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _opponentClone;
    private Collider _opponentCollider;
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
        _opponentCollider = _opponentClone.GetComponent<Collider>();
        _opponentClone.transform.position = new Vector3(_opponentClone.transform.position.x - 90f, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 opponentOriginPos = this.gameObject.transform.position;
        Vector3 opponentOriginRot = this.gameObject.transform.eulerAngles;

        opponentOriginPos.x -= 90f;

        RaycastHit hit;
        if (Physics.Raycast(_opponentClone.transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            if (hit.distance > 6f)
            {
                print(hit.distance);
                opponentOriginPos.y -= hit.distance;
            }
        }
        
        if (Physics.Raycast(_opponentClone.transform.position, transform.TransformDirection(Vector3.up), out hit))
        {
            if (hit.distance < -3f)
            {
                print(hit.distance);
                opponentOriginPos.y -= hit.distance;
            }
        }
        
        _opponentClone.transform.position = Vector3.Lerp(_opponentClone.transform.position, opponentOriginPos, Time.deltaTime);
    }
}
