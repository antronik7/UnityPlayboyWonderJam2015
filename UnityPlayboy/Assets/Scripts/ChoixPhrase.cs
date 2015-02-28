using UnityEngine;
using System.Collections;

public class ChoixPhrase : MonoBehaviour
{
    public int PositionY = 0;
    public string Phrase1 = "",
                  Phrase2 = "",
                  Phrase3 = "";
    private GUIText Texte;


    void Start()
    {
        Phrase1 = Phrase1.Insert(0, "   ");
        Phrase2 = Phrase2.Insert(0, "   ");
        Phrase3 = Phrase3.Insert(0, "   ");
        Texte = GetComponent<GUIText>();
        Texte.text = Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;
    }

	void FixedUpdate () 
    {
        Texte.text = Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;

        if (Input.GetAxis("Vertical") < 0 && PositionY != -2)
        {
            PositionY--;
        }
        else if (Input.GetAxis("Vertical") > 0 && PositionY != 0)
        {
            PositionY++;
        }

        switch (PositionY)
        {
            case 0:
                Phrase1 = Phrase1.Replace("   ", "-> ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -1:
                Phrase2 = Phrase2.Replace("   ", "-> ");
                Phrase1 = Phrase1.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -2:
                Phrase3 = Phrase3.Replace("   ", "-> ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase1 = Phrase1.Replace("-> ", "   ");
                break;
        }

        if (Input.GetKeyDown("space"))
            switch (PositionY)
           {
                case 0:
                    Debug.Log(Phrase1.Replace("-> ", ""));
                    break;
                case -1:
                    Debug.Log(Phrase2.Replace("-> ", ""));
                    break;
                case -2:
                    Debug.Log(Phrase3.Replace("-> ", ""));
                    break;
            }   
    }
}
