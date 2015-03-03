using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {
	
	public GameObject[] target;
	public GameObject[] table;
	public int personalite;
	public int columns = 20;
	public int rows = 13;

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();
	
	void InitialiseList()
	{
		gridPositions.Clear ();
		
		for(int x = 0; x < columns - 1; x++)
		{
			for(int y = 2; y < rows - 1; y++)
			{
				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	}
	// Use this for initialization
	void Start () {
		boardHolder = new GameObject ("Board").transform;
		InitialiseList ();
		LayoutObjectAtRandom (target, 12, 20);
		LayoutObjectAtRandom (table, 12, 20);
	}
	
	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}
	
	void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);
		
		for(int i = 0; i < objectCount; i ++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            if (tileChoice.tag == "Table" && randomPosition.x > 0 && randomPosition.x < 6 && randomPosition.y > 8 && randomPosition.y < 14)
            {

            }
            else
            {
                GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
