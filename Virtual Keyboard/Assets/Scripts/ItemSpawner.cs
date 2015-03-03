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

	// Send out a raycast from the camera's position. It should collide with some surface.
	// If that surface is the surface argument, return the position of the collision (where to spawn a new object)
	Vector3 calculateSpawnLocationOnSurface(GameObject surface) {
		RaycastHit hit;
		if (MainCamera != null) {
			if (surface != null) {
				if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out hit)) {
					if (surface == hit.collider.gameObject) {
						Debug.Log("CONTACT" + hit.point);
						return hit.point;
					}
				}
			}
		}
		return Vector3.zero;
	}

	Vector3 calculateSpawnLocation(Furniture.Mounting mounting) {
		switch(mounting) {
			case Furniture.Mounting.CEILING:
				return calculateSpawnLocationOnSurface(Ceiling);
				break;
			case Furniture.Mounting.FLOOR:
				return calculateSpawnLocationOnSurface(Floor);
				break;
			case Furniture.Mounting.WALL:
				Vector3 collisionSpot;
	    		for (int i = 0; i < 4; i++) {
					collisionSpot = calculateSpawnLocationOnSurface(Walls[i]);
					if (collisionSpot != Vector3.zero) {
						return collisionSpot;
					}
				}
				break;
		}
		return Vector3.zero;
	}

	void spawnFurniture(Furniture furniture) {
		Debug.Log ("THE SPAWNER HAS SPOKEN");
		Vector3 spawnLocation = calculateSpawnLocation(furniture.mounting);
		if (spawnLocation != Vector3.zero) {
			Quaternion rotationQ = Quaternion.Euler(furniture.rotation);
			GameObject obj = (GameObject)Instantiate(furniture.model, spawnLocation, rotationQ);
			obj.transform.localScale = furniture.scale;
		}
	}

}

