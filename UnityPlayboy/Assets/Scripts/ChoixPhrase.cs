using UnityEngine;
using System.Collections;

public class ChoixPhrase : MonoBehaviour
{
    public int PositionY;
	void Start () 
    {
        PositionY = 0;
	}
	
	void FixedUpdate () 
    {
        if (Input.GetKeyDown("down") && PositionY != -2)
            PositionY--;
        if (Input.GetKeyDown("up") && PositionY != 0)
            PositionY++;
        transform.position = new Vector3(0, PositionY, 0);
	}
}
