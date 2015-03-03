using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
	public GameObject Ceiling = null;
	public GameObject Floor = null;
	public GameObject[] Walls = new GameObject[4];
	

	// Use this for initialization
	void Start ()
	{
		FurnitureListItem.FurnitureCreated += spawnFurniture;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	Vector3 calculateSpawnLocation(Furniture.Mounting mounting) {
		switch(mounting) {
			case Furniture.Mounting.CEILING:
				if (Ceiling == null) { return Vector3.zero;}
				return Ceiling.transform.position;
				break;
			case Furniture.Mounting.FLOOR:
				if (Floor == null) { return Vector3.zero;}
				return Floor.transform.position;
				break;

			case Furniture.Mounting.WALL:
				//if (Ceiling == null) { return Vector3.zero;}
				return Vector3.zero;
				break;
		}
		return Vector3.zero;
	}

	void spawnFurniture(Furniture furniture) {
		Debug.Log ("THE SPAWNER HAS SPOKEN");
		Vector3 spawnLocation = calculateSpawnLocation(furniture.mounting);
		Quaternion rotationQ = Quaternion.Euler(furniture.rotation);
		GameObject obj = (GameObject)Instantiate(furniture.model, spawnLocation, rotationQ);
		obj.transform.localScale = furniture.scale;
	}

}

