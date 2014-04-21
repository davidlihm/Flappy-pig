using UnityEngine;
//using System;
using System.Collections;


public class GenerateWorld : MonoBehaviour {
	public GUIText scoreLabel;
	public Transform block;
	public Transform player;
	private float objectSpawnedTo = 7.0f;
	public static float distanceBetweenObjects = 10.5f;
	private float nextCheck = 0.0f;
	private ArrayList objects = new ArrayList();
	//private int[][,,,,] orderOfDirection = new int[][,,,,]{new int[1,0,1,0,1],new int[1,1,0,1,0],new int[0,1,0,0,1]};
	private int[,] orderOfDirection = new int[3,5]{ {1,0,1,0,1},{1,1,0,1,0},{0,1,0,0,1} };

	void Start () {
		//gameObject.light.transform.position

		maintenance(0.0f);
	}
	
	
	void Update () {
		//gameObject.light.transform.position = player.transform.position;
		float playerX = player.position.x;
		if(playerX > nextCheck)
		{
			maintenance(playerX);
		}

	}
	
	private void maintenance(float playerX)
	{
		nextCheck = playerX + 30;
		for (int i = objects.Count-1; i >= 0; i--) 
		{
			Transform blck = (Transform)objects[i];
			if(blck.position.x < (transform.position.x - 30))
			{
				Destroy(blck.gameObject);
				objects.RemoveAt(i);
			}
		}
		spawnObjects(5);
	}

	private int difficuty(int score)
	{
		return (score / 5);
		//return score;
	}

	private void spawnObjects(int howMany)
	{

		int randomDirection = Random.Range(0,3);
		int continued = 0;
		float spawnX = objectSpawnedTo;
		for(int i = 0; i<howMany; i++)
		{
			int x = int.Parse (scoreLabel.text);
			int diff = difficuty(x); //Difficuty of game. Raise with game process
			float largest = 12.0f + diff;
			bool direction = false;
			Vector3 pos;
			Transform blck;
			int directionInt = orderOfDirection[randomDirection,i];
			//direction = directionInt==1?true:false;
			if (directionInt == 1){direction = true;}
			float randomWidth = Random.Range(4f,6f);

			/*if (randomDirection == 1)
			{
				direction = true;
			}*/
			if (direction == true)
			{
				pos = new Vector3(spawnX, -2f-randomWidth/2, 0);
				 blck = (Transform)Instantiate(block, pos, Quaternion.identity);
				blck.localScale+=new Vector3(0,largest-randomWidth,0);
			}
			else
			{
				pos = new Vector3(spawnX, 11f-randomWidth, 0);
				 blck = (Transform)Instantiate(block, pos, Quaternion.identity);
				blck.localScale+=new Vector3(0,largest-randomWidth,0);
			}


			objects.Add(blck);
			//pos = new Vector3(spawnX, blck.position.y-blck.localScale.y-3.0f, 0);
			//blck = (Transform)Instantiate(block, pos, Quaternion.identity);
			//blck.localScale +=new Vector3(0,(8.6f-firstRandom)*1,0);//width
			//objects.Add(blck);
			spawnX = spawnX + distanceBetweenObjects;
		}
		objectSpawnedTo = spawnX;
	}
	
}
