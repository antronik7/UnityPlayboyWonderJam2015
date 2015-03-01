using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Font FontName;
	
    void OnGUI()
    {
        GUIStyle StyleName = new GUIStyle();
        StyleName.font = FontName;

        if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 4 + 30, 200, 50), "Jouer", StyleName))
			Application.LoadLevel ("MainAntoine");
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 350, 50), "Contrôles", StyleName))
					transform.position = new Vector3(0,0,-11);
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 3 * Screen.height / 4 - 30, 260, 50), "Quitter", StyleName))
			Application.Quit();
    }
}
