using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public bool tourJoueur1 = true;
	public int nbrActionMaxJoueur1 = 10;
	public int nbrActionMaxJoueur2 = 10;
	public int alcoholMax = 10;
	public int alcoholJoueur1 = 0;
	public int alcoholJoueur2 = 0;
	public GameObject joueur1;
	public GameObject joueur2;
	public bool joystickInUse = false;
	public float Speed = 1;
	public Camera mainCamera;
	public bool pasObstacle = true;
	public LayerMask blockingLayer;
	public int personaliteRecu = 0;
	public GameObject boxQuestions;
	Tests testsScript;
	targetManager targetManagerScript;
	tableManager tableManagerScript;

	private BoxCollider2D boxCollider;
	RaycastHit2D hit;
	Vector2 start;
	Vector2 end;

	// Use this for initialization
	void Start () {
		testsScript = boxQuestions.GetComponent<Tests> ();
	}
	
	// Update is called once per frame
	void Update () {

		float horizontal = 0;
		float vertical = 0;
		
		if (tourJoueur1 && Mathf.Abs(mainCamera.transform.position.x - joueur1.transform.position.x) < 1 && Mathf.Abs(mainCamera.transform.position.y - joueur1.transform.position.y) < 1) {
			if(nbrActionMaxJoueur1 > 0)
			{
				if (Input.GetKeyDown (KeyCode.UpArrow)) {

					if(joueur1.transform.position.y < 19)
					{
						boxCollider = joueur1.GetComponent<BoxCollider2D>();
						boxCollider.enabled = false;
						start = joueur1.transform.position;
						start.x = start.x + 0.5f;
						start.y = start.y + 0.5f;
						end = new Vector2(joueur1.transform.position.x + 0.5f, joueur1.transform.position.y + 1);

						hit = Physics2D.Linecast(start, end, blockingLayer);

						if(hit.transform == null)
						{
							joueur1.transform.position = new Vector2(joueur1.transform.position.x, joueur1.transform.position.y + 1);

							nbrActionMaxJoueur1--;
						}
						else if (hit.collider.tag == "Target")
						{
							Debug.Log(hit.collider.name);
							targetManagerScript = hit.collider.GetComponent<targetManager>();
							personaliteRecu = targetManagerScript.personalite;

							testsScript.Start();
							Debug.Log(personaliteRecu);
						}
						else if (hit.collider.tag == "Table")
						{
							tableManagerScript = hit.collider.GetComponent<tableManager>();
							if(tableManagerScript.drink())
							{
								if (alcoholJoueur1 < alcoholMax)
								{
									alcoholJoueur1++;
									Debug.Log(alcoholJoueur1);
									nbrActionMaxJoueur1--;
								}
							}
						}
						boxCollider.enabled = true;
					}
				} 
				else if (Input.GetKeyDown (KeyCode.DownArrow)) {
					if(joueur1.transform.position.y > 0)
					{
						boxCollider = joueur1.GetComponent<BoxCollider2D>();
						boxCollider.enabled = false;
						start = joueur1.transform.position;
						start.x = start.x + 0.5f;
						start.y = start.y + 0.5f;
						end = new Vector2(joueur1.transform.position.x + 0.5f, joueur1.transform.position.y - 0.9f);
						
						hit = Physics2D.Linecast(start, end, blockingLayer);

						if(hit.transform == null)
						{
							joueur1.transform.position = new Vector2(joueur1.transform.position.x, joueur1.transform.position.y - 1);
							nbrActionMaxJoueur1--;
						}
						else if (hit.collider.tag == "Target")
						{
							Debug.Log(hit.collider.name);
							targetManagerScript = hit.collider.GetComponent<targetManager>();
							personaliteRecu = targetManagerScript.personalite;
							Debug.Log(personaliteRecu);
						}
						else if (hit.collider.tag == "Table")
						{
							tableManagerScript = hit.collider.GetComponent<tableManager>();
							if(tableManagerScript.drink())
							{
								if (alcoholJoueur1 < alcoholMax)
								{
									alcoholJoueur1++;
									Debug.Log(alcoholJoueur1);
									nbrActionMaxJoueur1--;
								}
							}
						}
						boxCollider.enabled = true;
					}
				}
				else if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if(joueur1.transform.position.x < 19)
					{
						boxCollider = joueur1.GetComponent<BoxCollider2D>();
						boxCollider.enabled = false;
						start = joueur1.transform.position;
						start.x = start.x + 0.5f;
						start.y = start.y + 0.5f;
						end = new Vector2(joueur1.transform.position.x + 1, joueur1.transform.position.y + 0.5f);
						
						hit = Physics2D.Linecast(start, end, blockingLayer);

						if(hit.transform == null)
						{
							joueur1.transform.position = new Vector2(joueur1.transform.position.x + 1, joueur1.transform.position.y);
							nbrActionMaxJoueur1--;
						}
						else if (hit.collider.tag == "Target")
						{
							Debug.Log(hit.collider.name);
							targetManagerScript = hit.collider.GetComponent<targetManager>();
							personaliteRecu = targetManagerScript.personalite;
							Debug.Log(personaliteRecu);
						}
						else if (hit.collider.tag == "Table")
						{
							tableManagerScript = hit.collider.GetComponent<tableManager>();
							if(tableManagerScript.drink())
							{
								if (alcoholJoueur1 < alcoholMax)
								{
									alcoholJoueur1++;
									Debug.Log(alcoholJoueur1);
									nbrActionMaxJoueur1--;
								}
							}
						}
						boxCollider.enabled = true;
					}
				}
				else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if(joueur1.transform.position.x > 0)
					{
						boxCollider = joueur1.GetComponent<BoxCollider2D>();
						boxCollider.enabled = false;
						start = joueur1.transform.position;
						start.x = start.x + 0.5f;
						start.y = start.y + 0.5f;
						end = new Vector2(joueur1.transform.position.x - 0.9f, joueur1.transform.position.y + 0.5f);
						
						hit = Physics2D.Linecast(start, end, blockingLayer);

						if(hit.transform == null)
						{
							joueur1.transform.position = new Vector2(joueur1.transform.position.x - 1, joueur1.transform.position.y);
							nbrActionMaxJoueur1--;
						}
						else if (hit.collider.tag == "Target")
						{
							Debug.Log(hit.collider.name);
							targetManagerScript = hit.collider.GetComponent<targetManager>();
							personaliteRecu = targetManagerScript.personalite;
							Debug.Log(personaliteRecu);
						}
						else if (hit.collider.tag == "Table")
						{
							tableManagerScript = hit.collider.GetComponent<tableManager>();
							if(tableManagerScript.drink())
							{
								if (alcoholJoueur1 < alcoholMax)
								{
									alcoholJoueur1++;
									Debug.Log(alcoholJoueur1);
									nbrActionMaxJoueur1--;
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
				nbrActionMaxJoueur1 = 4;
			}
		} 
		else if (!tourJoueur1 && Mathf.Abs(mainCamera.transform.position.x - joueur2.transform.position.x) < 1 && Mathf.Abs(mainCamera.transform.position.y - joueur2.transform.position.y) < 1){
			if(nbrActionMaxJoueur2 > 0)
			{
				if(!(Input.GetKeyDown (KeyCode.LeftArrow)) && !(Input.GetKeyDown (KeyCode.RightArrow)) && !(Input.GetKeyDown (KeyCode.DownArrow)) && !(Input.GetKeyDown (KeyCode.DownArrow)))
				{
					horizontal = Input.GetAxis("Horizontal");
					vertical = Input.GetAxis("Vertical");
					
					if(horizontal == 1)
					{
						if(!joystickInUse)
						{
							if(joueur2.transform.position.x < 19)
							{
								boxCollider = joueur2.GetComponent<BoxCollider2D>();
								boxCollider.enabled = false;
								start = joueur2.transform.position;
								start.x = start.x + 0.5f;
								start.y = start.y + 0.5f;
								end = new Vector2(joueur2.transform.position.x + 1, joueur2.transform.position.y + 0.5f);
								
								hit = Physics2D.Linecast(start, end, blockingLayer);
								
								if(hit.transform == null)
								{
									joueur2.transform.position = new Vector2(joueur2.transform.position.x + 1, joueur2.transform.position.y);
									nbrActionMaxJoueur2--;

									joystickInUse = true;
								}
								boxCollider.enabled = true;
							}
						}
					}
					else if(horizontal == -1)
					{
						if(!joystickInUse)
						{
							if(joueur2.transform.position.x > 0)
							{
								boxCollider = joueur2.GetComponent<BoxCollider2D>();
								boxCollider.enabled = false;
								start = joueur2.transform.position;
								start.x = start.x + 0.5f;
								start.y = start.y + 0.5f;
								end = new Vector2(joueur2.transform.position.x - 0.9f, joueur2.transform.position.y + 0.5f);
								
								hit = Physics2D.Linecast(start, end, blockingLayer);
								
								if(hit.transform == null)
								{
									joueur2.transform.position = new Vector2(joueur2.transform.position.x - 1, joueur2.transform.position.y);
									nbrActionMaxJoueur2--;

									joystickInUse = true;
								}
								boxCollider.enabled = true;
							}
						}
					}
					else if(vertical == 1)
					{
						if(!joystickInUse)
						{
							if(joueur2.transform.position.y < 19)
							{
								boxCollider = joueur2.GetComponent<BoxCollider2D>();
								boxCollider.enabled = false;
								start = joueur2.transform.position;
								start.x = start.x + 0.5f;
								start.y = start.y + 0.5f;
								end = new Vector2(joueur2.transform.position.x + 0.5f, joueur2.transform.position.y + 1);
								
								hit = Physics2D.Linecast(start, end, blockingLayer);
								
								if(hit.transform == null)
								{
									joueur2.transform.position = new Vector2(joueur2.transform.position.x, joueur2.transform.position.y + 1);
									nbrActionMaxJoueur2--;

									joystickInUse = true;
								}
								boxCollider.enabled = true;
							}
						}
					}
					else if(vertical == -1)
					{
						if(!joystickInUse)
						{
							if(joueur2.transform.position.y > 0)
							{
								boxCollider = joueur2.GetComponent<BoxCollider2D>();
								boxCollider.enabled = false;
								start = joueur2.transform.position;
								start.x = start.x + 0.5f;
								start.y = start.y + 0.5f;
								end = new Vector2(joueur2.transform.position.x + 0.5f, joueur2.transform.position.y - 0.9f);
								
								hit = Physics2D.Linecast(start, end, blockingLayer);
								
								if(hit.transform == null)
								{
									joueur2.transform.position = new Vector2(joueur2.transform.position.x, joueur2.transform.position.y - 1);
									nbrActionMaxJoueur2--;

									joystickInUse = true;
								}
								boxCollider.enabled = true;
							}
						}
					}
					else if(horizontal == 0 && vertical == 0)
					{
						joystickInUse = false;
					}
				}
			}
			else
			{
				tourJoueur1 = true;
				nbrActionMaxJoueur2 = 4;
			}
		}

	}

	void FixedUpdate()
	{

	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds(10);
	}


}
