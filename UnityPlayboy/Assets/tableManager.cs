using UnityEngine;
using System.Collections;

public class tableManager : MonoBehaviour {

	public int drinkTable;
	public Sprite[] sprites;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool drink ()
	{
		if (drinkTable > 0) {
			drinkTable--;
			this.GetComponent<SpriteRenderer>().sprite = sprites[drinkTable];
			return true;
		}

		return false;
	}
}
