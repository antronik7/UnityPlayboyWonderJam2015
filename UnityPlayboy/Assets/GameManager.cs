using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool tourJoueur1 = true;
    public AudioClip beer;
    public int maxTurn = 20;
    public int nbTurn;
    public int degre = 0;
    public Sprite[] spritesBarStress;
    public Sprite[] spritseBarAlcool;
    public Sprite[] spritesBarMove;
    public AudioClip[] audioClips;
    public GameObject barStress1;
    public GameObject barAlcool1;
    public GameObject barStress2;
    public GameObject barAlcool2;
    public GameObject rectangle;
    public GameObject barMove1;
    public GameObject barMove2;
    public GameObject aiguile;
	public int nbrActionMaxJoueur1 = 10;
	public int nbrActionMaxJoueur2 = 10;
    public int nbrRepetion = 6;
	public int alcoholMax = 10;
	public int alcoholJoueur1 = 0;
	public int alcoholJoueur2 = 0;
    public int scoreJoueur1 = 0;
    public int scoreJoueur2 = 0;
	public GameObject joueur1;
	public GameObject joueur2;
    public int joueur1Stress = 0;
    public int joueur2Stress = 0;
	public bool joystickInUse = false;
    public bool phaseJeux = true;
    public bool peuxAfficher = true;
    public bool attendreMessage = false;
	public float Speed = 1;
	public Camera mainCamera;
	public bool pasObstacle = true;
	public LayerMask blockingLayer;
	public int personaliteRecu = 0;
    public string prenomRecu = "";
	public GameObject boxQuestions;
    public Font font;
    public GUIStyle style;
	Tests testsScript;
	targetManager targetManagerScript;
	tableManager tableManagerScript;

	private BoxCollider2D boxCollider;
	RaycastHit2D hit;
	Vector2 start;
	Vector2 end;

    enum sounds
    {
        music, beer, vomit, marche, doight
    }

	// Use this for initialization
	void Start () {
		testsScript = boxQuestions.GetComponent<Tests> ();
        rectangle.SetActive(false);
        style.font = font;
        style.fontSize = 12;
        nbTurn = maxTurn;
        OnGUI();
        afficheImage();

	}

    void playSound(sounds sound)
    {
        AudioSource.PlayClipAtPoint(audioClips[(int)sound], Camera.main.transform.position);
    }

    void afficheImage()
    {
        StartCoroutine(doFaireImage());
    }

    IEnumerator doFaireImage()
    {
        while (Input.anyKeyDown == false)
        {
            yield return null;// new WaitForSeconds(2);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("escape"))
        {
            Application.LoadLevel("Menu");
        }

        if (nbTurn < 1)
        {
            if (scoreJoueur1 > scoreJoueur2)
            {
                Application.LoadLevel("Player1Win");
            }
            else
            {
                Application.LoadLevel("Player2Win");
            }
        }

        if (phaseJeux)
        {
            float horizontal = 0;
            float vertical = 0;

            if (tourJoueur1 && Mathf.Abs(mainCamera.transform.position.x - joueur1.transform.position.x) < 1 && Mathf.Abs(mainCamera.transform.position.y - joueur1.transform.position.y) < 1)
            {
                if (nbrActionMaxJoueur1 > 0)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {

                        if (joueur1.transform.position.y < 19)
                        {
                            boxCollider = joueur1.GetComponent<BoxCollider2D>();
                            boxCollider.enabled = false;
                            start = joueur1.transform.position;
                            start.x = start.x + 0.5f;
                            start.y = start.y + 0.5f;
                            end = new Vector2(joueur1.transform.position.x + 0.5f, joueur1.transform.position.y + 1);

                            hit = Physics2D.Linecast(start, end, blockingLayer);

                            if (hit.transform == null)
                            {
                                playSound(sounds.marche);
                                joueur1.transform.position = new Vector2(joueur1.transform.position.x, joueur1.transform.position.y + 1);

                                nbrActionMaxJoueur1--;
                                if (nbrActionMaxJoueur1 <= 0)
                                    nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];

                            }
                            else if (hit.collider.tag == "Target")
                            {
                                playSound(sounds.doight);
                                targetManagerScript = hit.collider.GetComponent<targetManager>();
                                targetManagerScript.scoreJoueur = 0;
                                personaliteRecu = targetManagerScript.personalite;
                                prenomRecu = targetManagerScript.prenom;
                                nbrRepetion = 6;

                                joueur1.GetComponent<Animator>().SetTrigger("hitTarget");

                                phaseJeux = false;
                                rectangle.SetActive(true);
                                nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Table")
                            {
                                tableManagerScript = hit.collider.GetComponent<tableManager>();
                                if (tableManagerScript.drink())
                                {
                                    playSound(sounds.beer);
                                    if (alcoholJoueur1 < alcoholMax)
                                    {
                                        alcoholJoueur1++;
                                        if (alcoholJoueur1 >= 10)
                                            alcoholJoueur1 = 10;
                                        joueur1Stress -= drinkBeer_giveDelta();
                                        if (joueur1Stress <= 0)
                                            joueur1Stress = 0;

                                        barStress1.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur1Stress];
                                        barAlcool1.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur1];
                                        nbrActionMaxJoueur1--;
                                        if (nbrActionMaxJoueur1 <= 0)
                                            nbrActionMaxJoueur1 = 0;
                                        barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                                    }
                                    else
                                    {
                                        playSound(sounds.vomit);
                                    }
                                }
                            }
                            boxCollider.enabled = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        if (joueur1.transform.position.y > 0)
                        {
                            boxCollider = joueur1.GetComponent<BoxCollider2D>();
                            boxCollider.enabled = false;
                            start = joueur1.transform.position;
                            start.x = start.x + 0.5f;
                            start.y = start.y + 0.5f;
                            end = new Vector2(joueur1.transform.position.x + 0.5f, joueur1.transform.position.y - 0.9f);

                            hit = Physics2D.Linecast(start, end, blockingLayer);

                            if (hit.transform == null)
                            {
                                playSound(sounds.marche);
                                joueur1.transform.position = new Vector2(joueur1.transform.position.x, joueur1.transform.position.y - 1);
                                nbrActionMaxJoueur1--;
                                if (nbrActionMaxJoueur1 <= 0)
                                    nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Target")
                            {
                                playSound(sounds.doight);
                                targetManagerScript = hit.collider.GetComponent<targetManager>();
                                targetManagerScript.scoreJoueur = 0;
                                personaliteRecu = targetManagerScript.personalite;
                                prenomRecu = targetManagerScript.prenom;
                                nbrRepetion = 6;
                                joueur1.GetComponent<Animator>().SetTrigger("hitTarget");

                                phaseJeux = false;
                                rectangle.SetActive(true);
                                nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Table")
                            {
                                tableManagerScript = hit.collider.GetComponent<tableManager>();
                                if (tableManagerScript.drink())
                                {
                                    playSound(sounds.beer);
                                    if (alcoholJoueur1 < alcoholMax)
                                    {
                                        alcoholJoueur1++;
                                        if (alcoholJoueur1 >= 10)
                                            alcoholJoueur1 = 10;
                                        joueur1Stress -= drinkBeer_giveDelta();
                                        if (joueur1Stress <= 0)
                                            joueur1Stress = 0;

                                        barStress1.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur1Stress];
                                        barAlcool1.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur1];
                                        nbrActionMaxJoueur1--;
                                        if (nbrActionMaxJoueur1 <= 0)
                                            nbrActionMaxJoueur1 = 0;
                                        barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                                    }
                                    else
                                    {
                                        playSound(sounds.vomit);
                                    }
                                }
                            }
                            boxCollider.enabled = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (joueur1.transform.position.x < 19)
                        {
                            boxCollider = joueur1.GetComponent<BoxCollider2D>();
                            boxCollider.enabled = false;
                            start = joueur1.transform.position;
                            start.x = start.x + 0.5f;
                            start.y = start.y + 0.5f;
                            end = new Vector2(joueur1.transform.position.x + 1, joueur1.transform.position.y + 0.5f);

                            hit = Physics2D.Linecast(start, end, blockingLayer);

                            if (hit.transform == null)
                            {
                                playSound(sounds.marche);
                                joueur1.transform.position = new Vector2(joueur1.transform.position.x + 1, joueur1.transform.position.y);
                                nbrActionMaxJoueur1--;
                                if (nbrActionMaxJoueur1 <= 0)
                                    nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Target")
                            {
                                playSound(sounds.doight);
                                targetManagerScript = hit.collider.GetComponent<targetManager>();
                                targetManagerScript.scoreJoueur = 0;
                                personaliteRecu = targetManagerScript.personalite;
                                prenomRecu = targetManagerScript.prenom;
                                nbrRepetion = 6;

                                joueur1.GetComponent<Animator>().SetTrigger("hitTarget");

                                phaseJeux = false;
                                rectangle.SetActive(true);
                                nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Table")
                            {
                                tableManagerScript = hit.collider.GetComponent<tableManager>();
                                if (tableManagerScript.drink())
                                {
                                    playSound(sounds.beer);
                                    if (alcoholJoueur1 < alcoholMax)
                                    {
                                        alcoholJoueur1++;
                                        if (alcoholJoueur1 >= 10)
                                            alcoholJoueur1 = 10;
                                        joueur1Stress -= drinkBeer_giveDelta();
                                        if (joueur1Stress <= 0)
                                            joueur1Stress = 0;

                                        barStress1.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur1Stress];
                                        barAlcool1.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur1];
                                        nbrActionMaxJoueur1--;
                                        if (nbrActionMaxJoueur1 <= 0)
                                            nbrActionMaxJoueur1 = 0;
                                        barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                                    }
                                    else
                                    {
                                        playSound(sounds.vomit);
                                    }
                                }
                            }
                            boxCollider.enabled = true;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (joueur1.transform.position.x > 0)
                        {
                            boxCollider = joueur1.GetComponent<BoxCollider2D>();
                            boxCollider.enabled = false;
                            start = joueur1.transform.position;
                            start.x = start.x + 0.5f;
                            start.y = start.y + 0.5f;
                            end = new Vector2(joueur1.transform.position.x - 0.9f, joueur1.transform.position.y + 0.5f);

                            hit = Physics2D.Linecast(start, end, blockingLayer);

                            if (hit.transform == null)
                            {
                                playSound(sounds.marche);
                                joueur1.transform.position = new Vector2(joueur1.transform.position.x - 1, joueur1.transform.position.y);
                                nbrActionMaxJoueur1--;
                                if (nbrActionMaxJoueur1 <= 0)
                                    nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Target")
                            {
                                playSound(sounds.doight);
                                targetManagerScript = hit.collider.GetComponent<targetManager>();
                                targetManagerScript.scoreJoueur = 0;
                                personaliteRecu = targetManagerScript.personalite;
                                prenomRecu = targetManagerScript.prenom;
                                nbrRepetion = 6;

                                joueur1.GetComponent<Animator>().SetTrigger("hitTarget");

                                phaseJeux = false;
                                rectangle.SetActive(true);
                                nbrActionMaxJoueur1 = 0;
                                barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                            }
                            else if (hit.collider.tag == "Table")
                            {
                                tableManagerScript = hit.collider.GetComponent<tableManager>();
                                if (tableManagerScript.drink())
                                {
                                    playSound(sounds.beer);
                                    if (alcoholJoueur1 < alcoholMax)
                                    {
                                        alcoholJoueur1++;
                                        if (alcoholJoueur1 >= 10)
                                            alcoholJoueur1 = 10;
                                        joueur1Stress -= drinkBeer_giveDelta();
                                        if (joueur1Stress <= 0)
                                            joueur1Stress = 0;

                                        barStress1.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur1Stress];
                                        barAlcool1.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur1];
                                        nbrActionMaxJoueur1--;
                                        if (nbrActionMaxJoueur1 <= 0)
                                            nbrActionMaxJoueur1 = 0;
                                        barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                                    }
                                    else
                                    {
                                        playSound(sounds.vomit);
                                    }
                                }
                            }
                            boxCollider.enabled = true;
                        }
                    }
                }
                else
                {
                    tourJoueur1 = false;
                    nbrActionMaxJoueur1 = 5;
                    barMove1.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur1];
                }
            }
            else if (!tourJoueur1 && Mathf.Abs(mainCamera.transform.position.x - joueur2.transform.position.x) < 1 && Mathf.Abs(mainCamera.transform.position.y - joueur2.transform.position.y) < 1)
            {
                if (nbrActionMaxJoueur2 > 0)
                {
                    if (!(Input.GetKeyDown(KeyCode.W)) && !(Input.GetKeyDown(KeyCode.A)) && !(Input.GetKeyDown(KeyCode.S)) && !(Input.GetKeyDown(KeyCode.D)))
                    {
                        horizontal = Input.GetAxis("Horizontal");
                        vertical = Input.GetAxis("Vertical");

                        if (horizontal == 1 || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (!joystickInUse)
                            {
                                if (joueur2.transform.position.x < 19)
                                {
                                    boxCollider = joueur2.GetComponent<BoxCollider2D>();
                                    boxCollider.enabled = false;
                                    start = joueur2.transform.position;
                                    start.x = start.x + 0.5f;
                                    start.y = start.y + 0.5f;
                                    end = new Vector2(joueur2.transform.position.x + 1, joueur2.transform.position.y + 0.5f);

                                    hit = Physics2D.Linecast(start, end, blockingLayer);

                                    if (hit.transform == null)
                                    {
                                        playSound(sounds.marche);
                                        joueur2.transform.position = new Vector2(joueur2.transform.position.x + 1, joueur2.transform.position.y);
                                        nbrActionMaxJoueur2--;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                        if(!Input.GetKeyDown(KeyCode.RightArrow))
                                        {
                                            joystickInUse = true;
                                        }
                                    }
                                    else if (hit.collider.tag == "Target")
                                    {
                                        playSound(sounds.doight);
                                        targetManagerScript = hit.collider.GetComponent<targetManager>();
                                        targetManagerScript.scoreJoueur = 0;
                                        personaliteRecu = targetManagerScript.personalite;
                                        prenomRecu = targetManagerScript.prenom;
                                        nbrRepetion = 6;

                                        joueur2.GetComponent<Animator>().SetTrigger("hitTarget");

                                        phaseJeux = false;
                                        rectangle.SetActive(true);

                                        barStress2.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur2Stress];
                                        barAlcool2.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur2];
                                        nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];
                                    }
                                    else if (hit.collider.tag == "Table")
                                    {
                                        
                                        tableManagerScript = hit.collider.GetComponent<tableManager>();
                                        if (tableManagerScript.drink())
                                        {
                                            playSound(sounds.beer);
                                            if (alcoholJoueur2 < alcoholMax)
                                            {
                                                alcoholJoueur2++;
                                                if (alcoholJoueur2 >= 10)
                                                    alcoholJoueur2 = 10;
                                                joueur2Stress -= drinkBeer_giveDelta();
                                                if (joueur2Stress <= 0)
                                                    joueur2Stress = 0;
                                                nbrActionMaxJoueur2--;
                                                if (nbrActionMaxJoueur2 <= 0)
                                                    nbrActionMaxJoueur2 = 0;
                                                barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                                joystickInUse = true;
                                            }
                                        }
                                    }
                                    boxCollider.enabled = true;
                                }
                            }
                        }
                        else if (horizontal == -1 || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (!joystickInUse)
                            {
                                if (joueur2.transform.position.x > 0)
                                {
                                    boxCollider = joueur2.GetComponent<BoxCollider2D>();
                                    boxCollider.enabled = false;
                                    start = joueur2.transform.position;
                                    start.x = start.x + 0.5f;
                                    start.y = start.y + 0.5f;
                                    end = new Vector2(joueur2.transform.position.x - 0.9f, joueur2.transform.position.y + 0.5f);

                                    hit = Physics2D.Linecast(start, end, blockingLayer);

                                    if (hit.transform == null)
                                    {
                                        playSound(sounds.marche);
                                        joueur2.transform.position = new Vector2(joueur2.transform.position.x - 1, joueur2.transform.position.y);
                                        nbrActionMaxJoueur2--;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                        if (!Input.GetKeyDown(KeyCode.LeftArrow))
                                        {
                                            joystickInUse = true;
                                        }
                                    }
                                    else if (hit.collider.tag == "Target")
                                    {
                                        playSound(sounds.doight);
                                        targetManagerScript = hit.collider.GetComponent<targetManager>();
                                        targetManagerScript.scoreJoueur = 0;
                                        personaliteRecu = targetManagerScript.personalite;
                                        prenomRecu = targetManagerScript.prenom;
                                        nbrRepetion = 6;

                                        joueur2.GetComponent<Animator>().SetTrigger("hitTarget");

                                        phaseJeux = false;
                                        rectangle.SetActive(true);
                                        nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];
                                    }
                                    else if (hit.collider.tag == "Table")
                                    {
                                        
                                        tableManagerScript = hit.collider.GetComponent<tableManager>();
                                        if (tableManagerScript.drink())
                                        {
                                            playSound(sounds.beer);
                                            if (alcoholJoueur2 < alcoholMax)
                                            {
                                                alcoholJoueur2++;
                                                if (alcoholJoueur2 >= 10)
                                                    alcoholJoueur2 = 10;
                                                joueur2Stress -= drinkBeer_giveDelta();
                                                if (joueur2Stress <= 0)
                                                    joueur2Stress = 0;

                                                barStress2.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur2Stress];
                                                barAlcool2.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur2];
                                                nbrActionMaxJoueur2--;
                                                if (nbrActionMaxJoueur2 <= 0)
                                                    nbrActionMaxJoueur2 = 0;
                                                barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                                joystickInUse = true;
                                            }
                                        }
                                    }
                                    boxCollider.enabled = true;
                                }
                            }
                        }
                        else if (vertical == 1 || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (!joystickInUse)
                            {
                                if (joueur2.transform.position.y < 19)
                                {
                                    boxCollider = joueur2.GetComponent<BoxCollider2D>();
                                    boxCollider.enabled = false;
                                    start = joueur2.transform.position;
                                    start.x = start.x + 0.5f;
                                    start.y = start.y + 0.5f;
                                    end = new Vector2(joueur2.transform.position.x + 0.5f, joueur2.transform.position.y + 1);

                                    hit = Physics2D.Linecast(start, end, blockingLayer);

                                    if (hit.transform == null)
                                    {
                                        playSound(sounds.marche);
                                        joueur2.transform.position = new Vector2(joueur2.transform.position.x, joueur2.transform.position.y + 1);
                                        nbrActionMaxJoueur2--;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                        if (!Input.GetKeyDown(KeyCode.UpArrow))
                                        {
                                            joystickInUse = true;
                                        }
                                    }
                                    else if (hit.collider.tag == "Target")
                                    {
                                        playSound(sounds.doight);
                                        targetManagerScript = hit.collider.GetComponent<targetManager>();
                                        targetManagerScript.scoreJoueur = 0;
                                        personaliteRecu = targetManagerScript.personalite;
                                        prenomRecu = targetManagerScript.prenom;
                                        nbrRepetion = 6;

                                        joueur2.GetComponent<Animator>().SetTrigger("hitTarget");

                                        phaseJeux = false;
                                        rectangle.SetActive(true);
                                        nbrActionMaxJoueur2 = 0;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];
                                    }
                                    else if (hit.collider.tag == "Table")
                                    {
                                        
                                        tableManagerScript = hit.collider.GetComponent<tableManager>();
                                        if (tableManagerScript.drink())
                                        {
                                            playSound(sounds.beer);
                                            if (alcoholJoueur2 < alcoholMax)
                                            {
                                                alcoholJoueur2++;
                                                if (alcoholJoueur2 >= 10)
                                                    alcoholJoueur2 = 10;
                                                joueur2Stress -= drinkBeer_giveDelta();
                                                if (joueur2Stress <= 0)
                                                    joueur2Stress = 0;
                                                alcoholJoueur1++;

                                                barStress2.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur2Stress];
                                                barAlcool2.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur2];
                                                nbrActionMaxJoueur2--;
                                                if (nbrActionMaxJoueur2 <= 0)
                                                    nbrActionMaxJoueur2 = 0;
                                                barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                                joystickInUse = true;
                                            }
                                        }
                                    }
                                    boxCollider.enabled = true;
                                }
                            }
                        }
                        else if (vertical == -1 || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (!joystickInUse)
                            {
                                if (joueur2.transform.position.y > 0)
                                {
                                    boxCollider = joueur2.GetComponent<BoxCollider2D>();
                                    boxCollider.enabled = false;
                                    start = joueur2.transform.position;
                                    start.x = start.x + 0.5f;
                                    start.y = start.y + 0.5f;
                                    end = new Vector2(joueur2.transform.position.x + 0.5f, joueur2.transform.position.y - 0.9f);

                                    hit = Physics2D.Linecast(start, end, blockingLayer);

                                    if (hit.transform == null)
                                    {
                                        playSound(sounds.marche);
                                        joueur2.transform.position = new Vector2(joueur2.transform.position.x, joueur2.transform.position.y - 1);
                                        nbrActionMaxJoueur2--;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                        if (!Input.GetKeyDown(KeyCode.DownArrow))
                                        {
                                            joystickInUse = true;
                                        }
                                    }
                                    else if (hit.collider.tag == "Target")
                                    {
                                        playSound(sounds.doight);
                                        targetManagerScript = hit.collider.GetComponent<targetManager>();
                                        targetManagerScript.scoreJoueur = 0;
                                        personaliteRecu = targetManagerScript.personalite;
                                        prenomRecu = targetManagerScript.prenom;
                                        nbrRepetion = 6;

                                        joueur2.GetComponent<Animator>().SetTrigger("hitTarget");

                                        phaseJeux = false;
                                        rectangle.SetActive(true);
                                        nbrActionMaxJoueur2 = 0;
                                        if (nbrActionMaxJoueur2 <= 0)
                                            nbrActionMaxJoueur2 = 0;
                                        barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];
                                    }
                                    else if (hit.collider.tag == "Table")
                                    {
                                        
                                        tableManagerScript = hit.collider.GetComponent<tableManager>();
                                        if (tableManagerScript.drink())
                                        {
                                            playSound(sounds.beer);
                                            if (alcoholJoueur2 < alcoholMax)
                                            {
                                                
                                                alcoholJoueur2++;
                                                if (alcoholJoueur2 >= 10)
                                                    alcoholJoueur2 = 10;
                                                joueur2Stress -= drinkBeer_giveDelta();
                                                if (joueur2Stress <= 0)
                                                    joueur2Stress = 0;

                                                barStress2.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur2Stress];
                                                barAlcool2.GetComponent<SpriteRenderer>().sprite = spritseBarAlcool[alcoholJoueur2];
                                                nbrActionMaxJoueur2--;
                                                if (nbrActionMaxJoueur2 <= 0)
                                                    nbrActionMaxJoueur2 = 0;
                                                barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];

                                                joystickInUse = true;
                                            }
                                        }
                                    }
                                    boxCollider.enabled = true;
                                }
                            }
                        }
                        else if (horizontal == 0 && vertical == 0)
                        {
                            joystickInUse = false;
                        }
                    }
                }
                else
                {
                    tourJoueur1 = true;
                    horloge();
                    nbrActionMaxJoueur2 = 5;
                    barMove2.GetComponent<SpriteRenderer>().sprite = spritesBarMove[nbrActionMaxJoueur2];
                }

            }
        }
        else
        {
            float horizontal = 0;
            float vertical = 0;
            
            if (peuxAfficher)
            {
                testsScript.afficherTexte();
                peuxAfficher = false;
                attendreMessage = false;
            }

            if (tourJoueur1)
            {
                if (nbrRepetion > 0)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A))
                    {
                        testsScript.gererInput(1);
                    }
                    else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    {
                        testsScript.gererInput(2);
                    }
                    else if (Input.GetKeyDown("space"))
                    {
                       
                        nbrRepetion--;
                        if (attendreMessage == false)
                        {
                            testsScript.gererInput(3);
                            joueur1Stress += testsScript.addToStressValue();

                            
                            targetManagerScript.addToPersonalScore( testsScript.giveScore(testsScript.ValeurRetournee));


                            if (joueur1Stress > 10)
                            {
                                joueur1Stress = 10;
                            }

                            barStress1.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur1Stress];
                            peuxAfficher = false;
                            attendreMessage = true;
                        }
                        else {
                            
                            attendreMessage = false;
                            peuxAfficher = true;
                        }
                        
                    }
                }
                else
                {
                    addScoreToPlayer(targetManagerScript.getFinalScore(), tourJoueur1);
                    testsScript.clearTextBox();
                    peuxAfficher = true;
                    tourJoueur1 = false;
                    nbrActionMaxJoueur1 = 5;
                    phaseJeux = true;
                    rectangle.SetActive(false);
                }
            }
            else
            {
                if (nbrRepetion > 0)
                {
                    if (!(Input.GetKeyDown(KeyCode.W)) && !(Input.GetKeyDown(KeyCode.A)) && !(Input.GetKeyDown(KeyCode.S)) && !(Input.GetKeyDown(KeyCode.D)))
                    {
                        vertical = Input.GetAxis("Vertical");
                        horizontal = Input.GetAxis("Horizontal");

                        if ((vertical == 1 && !joystickInUse) || (horizontal == -1 && !joystickInUse) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            testsScript.gererInput(1);
                            joystickInUse = true;
                        }
                        else if ((vertical == -1 && !joystickInUse) || (horizontal == 1 && !joystickInUse) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            testsScript.gererInput(2);
                            joystickInUse = true;
                        }
                        else if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Return))
                        {
                            nbrRepetion--;
                            if (attendreMessage == false)
                            {
                                testsScript.gererInput(3);
                                joueur2Stress += testsScript.addToStressValue();
                                targetManagerScript.addToPersonalScore(testsScript.giveScore(testsScript.ValeurRetournee));
                        
                                if (joueur2Stress > 10)
                                {
                                    joueur2Stress = 10;
                                }

                                barStress2.GetComponent<SpriteRenderer>().sprite = spritesBarStress[joueur2Stress];
                                peuxAfficher = false;
                                attendreMessage = true;
                            }
                            else
                            {

                                attendreMessage = false;
                                peuxAfficher = true;
                            }
                        }
                        else if (horizontal == 0 && vertical == 0)
                        {
                            joystickInUse = false;
                        }
                    }
                }
                else
                {
                    addScoreToPlayer(targetManagerScript.getFinalScore(), tourJoueur1);
                    testsScript.clearTextBox();
                    peuxAfficher = true;
                    tourJoueur1 = true;
                    nbrActionMaxJoueur2 = 5;
                    horloge();
                    phaseJeux = true;
                    rectangle.SetActive(false);
                    
                }
            }
        }
	}

    public int drinkBeer_giveDelta() 
{
    int newStressDelta;
    newStressDelta = Random.Range(1, 4);
	return newStressDelta;
}
    public void addScoreToPlayer( int scoreDeLaPute , bool tourjoueur1 )
    {
        if (tourjoueur1)
        {
            if (scoreDeLaPute == 2)
            {
                //Perfect
                scoreJoueur1 += 150;
                StartCoroutine(waitBeforeGo());
            }
            else if (scoreDeLaPute == 1)
            {
                //Tu la juste chope posey
                scoreJoueur1 += 100;
                StartCoroutine(waitBeforeGo());
            }
            else if ( scoreDeLaPute == -1 )
            {
                //Elle t'as foutu un gros rateau 
                scoreJoueur1 -= 50;
                StartCoroutine(waitBeforeGo());
            }
        }
        else
        {
            if (scoreDeLaPute == 2)
            {
                //Perfect
                scoreJoueur2 += 150;
                StartCoroutine(waitBeforeGo());
            }
            else if (scoreDeLaPute == 1)
            {
                //Tu la juste chope posey
                scoreJoueur2 += 100;
                StartCoroutine(waitBeforeGo());
            }
            else if (scoreDeLaPute == -1)
            {
                //Elle t'as foutu un gros rateau 
                scoreJoueur2 -= 50;
                StartCoroutine(waitBeforeGo());
            }

        }

        OnGUI();
    }

    IEnumerator waitBeforeGo()
    {
        yield return new WaitForSeconds(1);
        hit.collider.GetComponent<Transform>().position = new Vector3(100, 100, 100);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 50, 50), "Score Joueur 1 : " + scoreJoueur1.ToString(), style);
        GUI.Label(new Rect(Screen.width - 260, 10, 50, 50), "Score Joueur 2 : " + scoreJoueur2.ToString(), style);
    }

    void horloge()
    {
        nbTurn--;
        degre += (360 / maxTurn) * (-1);

        aiguile.transform.localEulerAngles = new Vector3(0, 0, degre);
    }

}
