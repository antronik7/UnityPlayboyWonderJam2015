using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Font FontName;

    void OnGUI()
    {
        GUIStyle StyleName = new GUIStyle();
        StyleName.font = FontName;

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 4 + 30, 200, 50), "Jouer", StyleName))
            Debug.Log("Bouton \"Jouer\" appuyé! :D");
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 350, 50), "Contrôles", StyleName))
            Debug.Log("Bouton \"Contrôles\" appuyé! :D");
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 3 * Screen.height / 4 - 30, 260, 50), "Quitter", StyleName))
            Debug.Log("Bouton \"Quitter\" appuyé! :D");
    }
}
