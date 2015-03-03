using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
	public GameObject Ceiling = null;
	public GameObject Floor = null;
	public GameObject[] Walls = new GameObject[4];
	public GameObject MainCamera = null;

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
				if (Ceiling == null) { return Vector3.zero; }
				return Ceiling.transform.position;
				break;
			case Furniture.Mounting.FLOOR:
				if (Floor == null) { return Vector3.zero; }
				return Floor.transform.position;
				break;

			case Furniture.Mounting.WALL:
				if (MainCamera != null) {
					// Send out a raycast from the camera's position, if it collides with a wall, return the wall's position.
					RaycastHit hit;
					if(Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit)) {
	    				for (int i = 0; i < 4; i++) {
							if (Walls[i] == null) { continue; }
							if (Walls[i].transform.position == hit.transform.position) {
								Debug.Log("Collision at" + Walls[i].transform.position);
								return Walls[i].transform.position;
							}
						}
					}
				}

				Debug.Log("No collisions with walls were recorded");
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

