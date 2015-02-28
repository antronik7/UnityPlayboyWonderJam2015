using UnityEngine;
using System.Collections;

public class targetManager : MonoBehaviour {

	public int personalite;
	public int scoreMax = 10;
	public int scoreJoueur1 = 0;
	public int scoreJoueur2 = 0;
	// Use this for initialization
	void Start () {
		personalite = Random.Range (0, 10);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public bool changeScore(int scoreObtenu, bool tourJoueur1)
	{
		if (tourJoueur1) {
			scoreJoueur1 = scoreJoueur1 + scoreObtenu;

			if (scoreJoueur1 > scoreMax) {
				return true;
			}
		} 
		else 
		{
			scoreJoueur2 = scoreJoueur2 + scoreObtenu;
			
			if (scoreJoueur2 > scoreMax) {
				return true;
			}
		}

		return false;
	}
}
