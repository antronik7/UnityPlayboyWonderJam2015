using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform Player;
	public Transform joueur1;
	public Transform joueur2;
	public Vector2 Smoothing;

	GameObject gameManager;
	GameManager gameManagerScript;

	// Use this for initialization
	void Start () {
		Smoothing.x = 2;
		Smoothing.y = 2;
		gameManager = GameObject.Find("GameManager");
		gameManagerScript = gameManager.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManagerScript.tourJoueur1) {
			Player = joueur1;
		} 
		else {
			Player = joueur2;
		}
		var x = transform.position.x;
		var y = transform.position.y;

		x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
		y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);

		transform.position = new Vector3 (x, y, transform.position.z);
	}
}
