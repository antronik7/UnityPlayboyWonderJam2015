using UnityEngine;
using System.Collections;

public class ChoixPhrase : MonoBehaviour
{
    public int PositionY;
    public bool AnimationFin;
    public GUIText Texte;

    void Start()
    {
        Texte = GetComponentInParent<GUIText>();
        Texte.text = "Autre phrase\n2\n3";
    }

	void Update () 
    {
        if (transform.position.y == 0.6)
        {
            AnimationFin = true;
            PositionY = 0;
        }
        if (AnimationFin)
        {
            if (Input.GetKeyDown("down") && PositionY != -2)
                PositionY--;
            if (Input.GetKeyDown("up") && PositionY != 0)
                PositionY++;
            transform.position = new Vector3(-2, PositionY, 0);
        } 
    }
}
