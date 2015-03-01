using UnityEngine;
using System.Collections;

public class targetManager : MonoBehaviour {

	public int personalite;
	public int scoreMax = 10;
	public int scoreJoueur = 0;
    public string[] prenoms = new string[] { "Alex", "Charlie", "Maxime", "Robyn", "Kim", "Yannick", "Clarence", "Claude", "Dominique", "Frédérick", "Bénédicte", "Gwenn", "Noah", "Allison", "Aurèle", "Brice", "Carol", "Calixte", "Carmel", "Chris", "Nico", "Domo", "Tony", "Sim", "Mat", "Claris", "Dali", "Laurence", "Dany", "Elijah", "Edith", "Elly", "Eddy", "Enzo", "Esther", "Gaby", "Guillaume", "Hadil", "Heaven", "Hamara", "Hyacinthe", "Ingrid", "Isidore", "Jany", "Jazz", "Janick", "Jade", "Jemmy", "Jo", "Jillian", "Jessie", "Joan", "Jimmy", "Justin", "Kay", "Lee", "Lilo", "Lindsay", "Lesly", "Lois", "Mel", "Nell", "Nguyen", "Noa", "Olga", "Paris", "Quentin", "Raphael", "Rémy", "René", "Rosaire", "Sally", "Said", "Sammy", "Saturnin", "Serge", "Sully", "Symphorien", "Ulysse", "Wally", "Yann", "Zola", "Zoe" };
    public string prenom;
	// Use this for initialization
	void Start () {
        if(personalite != 10)
		    personalite = Random.Range (0, 10);
        prenom = prenoms[Random.Range(0, prenoms.Length)];
	}

    public void addToPersonalScore( int val ) 
    {
            scoreJoueur += val;
    }

	public int getFinalScore()
	{
			if (scoreJoueur == scoreMax) {
				return 2;
			}
            else if (scoreJoueur >= 3 && scoreJoueur < scoreMax )
            {
                return 1;
            }
            else if (scoreJoueur < 3 && scoreJoueur > -3)
            {
                return 0;
            }
            else 
            {
                return -1;
            }
	}
}
