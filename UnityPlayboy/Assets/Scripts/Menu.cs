using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Font FontName;
    void OnGUI()
    {
		GUIStyle StyleName = new GUIStyle ();
		StyleName.font = FontName;
		StyleName.fontSize = (20);
		StyleName.normal.textColor = Color.white;

		if (transform.position.y == 0) {
			if (GUI.Button (new Rect (Screen.width / 2 - 70, Screen.height / 4 + 220, 200, 50), "Jouer", StyleName)) {
                transform.position = new Vector3 (0, 16, -11);
                StartCoroutine(showInstruction());
                
			}
			if (GUI.Button (new Rect (Screen.width / 2 - 110, Screen.height / 4 + 270, 350, 50), "Contrôles", StyleName))
				transform.position = new Vector3 (0, 8, -11);
			if (GUI.Button (new Rect (Screen.width / 2 - 140, Screen.height / 4 + 320, 350, 50), "Instructions", StyleName))
				transform.position = new Vector3 (0, -8, -11);
			if (GUI.Button (new Rect (Screen.width / 2 - 90, Screen.height / 4 + 370, 260, 50), "Quitter", StyleName))
				Application.Quit ();
		} else if (transform.position.y == 8) {
			if (GUI.Button (new Rect (Screen.width / 2 - 80, Screen.height-45, 350, 50), "Retour", StyleName))
				transform.position = new Vector3 (0, 0, -11);
		} else if (transform.position.y == -8)
		{
            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height - 45, 350, 50), "Retour", StyleName))
				transform.position = new Vector3 (0, 0, -11);
		}
	}

    IEnumerator showInstruction()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("MainAntoine");
    }
}
