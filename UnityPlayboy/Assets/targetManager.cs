using UnityEngine;
using System.Collections;

public class targetManager : MonoBehaviour {

	public int personalite;
    public AudioClip[] audioClips;
    public Sprite[] sprites;
	public int scoreMax = 10;
	public int scoreJoueur = 0;
    public string[] prenoms = new string[] { "Alex", "Charlie", "Maxime", "Robyn", "Kim", "Yannick", "Clarence", "Claude", "Dominique", "Frédérick", "Bénédicte", "Gwenn", "Noah", "Allison", "Aurèle", "Brice", "Carol", "Calixte", "Carmel", "Chris", "Nico", "Domo", "Tony", "Sim", "Mat", "Claris", "Dali", "Laurence", "Dany", "Elijah", "Edith", "Elly", "Eddy", "Enzo", "Esther", "Gaby", "Guillaume", "Hadil", "Heaven", "Hamara", "Hyacinthe", "Ingrid", "Isidore", "Jany", "Jazz", "Janick", "Jade", "Jemmy", "Jo", "Jillian", "Jessie", "Joan", "Jimmy", "Justin", "Kay", "Lee", "Lilo", "Lindsay", "Lesly", "Lois", "Mel", "Nell", "Nguyen", "Noa", "Olga", "Paris", "Quentin", "Raphael", "Rémy", "René", "Rosaire", "Sally", "Said", "Sammy", "Saturnin", "Serge", "Sully", "Symphorien", "Ulysse", "Wally", "Yann", "Zola", "Zoe" };
    public string prenom;
	// Use this for initialization

    enum sounds
    {
        kiss, tape
    }

    void playSound(sounds sound)
    {
        AudioSource.PlayClipAtPoint(audioClips[(int)sound], Camera.main.transform.position);
    }

	void Start () {
        if(personalite != 10)
		    personalite = Random.Range (0, 10);
        prenom = prenoms[Random.Range(0, (int)prenoms.Length)];
	}

    public void addToPersonalScore( int val ) 
    {
            scoreJoueur += val;

            StartCoroutine(afficherFaces(val));
    }

	public int getFinalScore()
	{
			if (scoreJoueur == scoreMax) {
                StartCoroutine(afficherSprite());
				return 2;
			}
            else if (scoreJoueur >= 3 && scoreJoueur < scoreMax )
            {
                StartCoroutine(afficherSprite());
                return 1;
            }
            else if (scoreJoueur < 3 && scoreJoueur > -3)
            {
                StartCoroutine(afficherSprite());
                return 0;
            }
            else 
            {
                StartCoroutine(afficherSprite());
                return -1;
            }
	}

    IEnumerator afficherSprite()
    {
        if (scoreJoueur == scoreMax)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[3];
            playSound(sounds.kiss);
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (scoreJoueur >= 3 && scoreJoueur < scoreMax)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[3];
            playSound(sounds.kiss);
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (scoreJoueur < 3 && scoreJoueur > -3)
        {
            yield return new WaitForSeconds(1);
            
        }
        else
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[4];
            playSound(sounds.tape);
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    IEnumerator afficherFaces(int val)
    {
        if (val == 2)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (val == -1)
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
            yield return new WaitForSeconds(1);
            this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
