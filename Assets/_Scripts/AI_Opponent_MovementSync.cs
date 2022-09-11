using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Opponent_MovementSync : MonoBehaviour
{
    //Container for the opponent clone present on our board.
    private GameObject _opponentClone;
    
    //Reference to opponent's collider.
    private Collider _opponentCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        //Find all gameObjects in scene with the tag "Opponent"
        GameObject[] thingyToFind = GameObject.FindGameObjectsWithTag("Opponent");

        //If none were found, or the number found was not exactly 1, proceed
        if (thingyToFind == null || thingyToFind.Length != 1)
        {
            //Destroy this script to prevent an infinite loop of opponents.
            Destroy(this);
            return;
        }
        //Instantiate (create) a new instance of opponent prefab
        _opponentClone = Instantiate(this.gameObject);
        
        //Get collider of newly created Opponent.
        _opponentCollider = _opponentClone.GetComponent<Collider>();
        
        //Move opponent to our board.
        _opponentClone.transform.position = new Vector3(_opponentClone.transform.position.x - 90f, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get opponent position on their field.
        Vector3 opponentOriginPos = this.gameObject.transform.position;

        //Apply an offset of -90, as the opponent's arena is located -90 from 0,0,0.
        //Constantly applying this offset will allow us to accurately project the opponents movements onto our player field.
        opponentOriginPos.x -= 90f;

        //Create container for Raycast Hit
        //A Raycast is a laser which will return data on the first object hit.
        //Read more at: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
        RaycastHit hit;
        
        //Fire raycast from clone position. Fire it downwards, assign data to "hit" variable above.
        if (Physics.Raycast(_opponentClone.transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            //if distance between ball and floor is greater than 6, proceed.
            //if its less than 6, movement won't really be noticeable.
            if (hit.distance > 6f)
            {
                //offset opponent Y-position to match to our floor.
                opponentOriginPos.y -= hit.distance;
            }
        }
        
        //NEEDS TO BE REVISED!
        //Same as above, but fire raycast up, and place it back onto the platform if it clips through.
        if (Physics.Raycast(_opponentClone.transform.position, transform.TransformDirection(Vector3.up), out hit))
        {
            if (hit.distance < -3f)
            {
                opponentOriginPos.y += hit.distance + 2f;
            }
        }
        
        //Lerp position by time.deltaTime
        //Read more about Lerp here: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        _opponentClone.transform.position = Vector3.Lerp(_opponentClone.transform.position, opponentOriginPos, Time.deltaTime);
    }
}
