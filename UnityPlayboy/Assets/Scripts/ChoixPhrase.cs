using UnityEngine;
using System.Collections;

public class ChoixPhrase : MonoBehaviour
{
    public int PositionY;
    public bool AnimationFin;
    public string Phrase1 = "Chien",
                  Phrase2 = "Horloge Grand-père",
                  Phrase3 = "Hublot";
    private GUIText Texte;
    private Animator test;

    void Start()
    {
        Texte = GetComponentInParent<GUIText>();
        Texte.text = Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;
    }

	void Update () 
    {
        if (transform.position.y >= 0 && AnimationFin == false)
        {
            AnimationFin = true;
            test = GetComponent<Animator>();
            test.enabled = false;  
        }

        if (AnimationFin)
        {
            if (Input.GetKeyDown("down") && PositionY != -2)
                PositionY--;
            else if (Input.GetKeyDown("up") && PositionY != 0)
                PositionY++;
            transform.position = new Vector3(-2, PositionY, 0);
            if (Input.GetKeyDown("space"))
                switch (PositionY)
                {
                    case 0:
                        Debug.Log(Phrase1);
                        break;
                    case -1:
                        Debug.Log(Phrase2);
                        break;
                    case -2:
                        Debug.Log(Phrase3);
                        break;
                }
        } 
    }
}
