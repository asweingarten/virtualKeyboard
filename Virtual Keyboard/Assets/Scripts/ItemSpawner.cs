using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
	public GameObject Ceiling = null;
	public GameObject Floor = null;
	public GameObject[] Walls = new GameObject[4];
	public GameObject MainCamera = null;

	private float lastSpawnTime = 0f;

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
		Vector3 spawnLocation = surface.renderer.bounds.center;
		spawnLocation.y = surface.transform.position.y;
		return spawnLocation;
	}

	Vector3 calculateSpawnLocation(Furniture.Mounting mounting) {
		switch(mounting) {
			case Furniture.Mounting.CEILING:
				return calculateSpawnLocationOnSurface(Ceiling);
			case Furniture.Mounting.FLOOR:
				return calculateSpawnLocationOnSurface(Floor);
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

	void addTransformationManager(GameObject gameObject, Furniture.Mounting mounting) {
		gameObject.AddComponent<TransformationManager>();
		TransformationManager tm = gameObject.GetComponent<TransformationManager> ();
		switch (mounting) {
		case Furniture.Mounting.CEILING:
			tm.canTranslateX = true;
			tm.canTranslateZ = true;
			tm.canRotateX = true;
			tm.canRotateZ = true;
			tm.canScaleX = true;
			tm.canScaleZ = true;
			break;

		case Furniture.Mounting.FLOOR:
			tm.canTranslateX = true;
			tm.canTranslateZ = true;
			tm.canRotateX = true;
			tm.canRotateZ = true;
			tm.canScaleX = true;
			tm.canScaleZ = true;
			break;

		case Furniture.Mounting.WALL:
			// @TODO: determine the plane of the wall and set values accordingly
			break;
		}
	}

	void spawnFurniture(Furniture furniture) {
		if(Time.time - lastSpawnTime < 3f ) return;
		lastSpawnTime = Time.time;
		Debug.Log ("THE SPAWNER HAS SPOKEN");
		Vector3 spawnLocation = calculateSpawnLocation(furniture.mounting);
		if (spawnLocation != Vector3.zero) {
			Debug.Log ("Should create onject");
			Quaternion rotationQ = Quaternion.Euler(furniture.rotation);
			GameObject obj = (GameObject)Instantiate(furniture.model, spawnLocation, rotationQ);

			obj.transform.localScale = furniture.scale;

			GameObject furnitureObject = obj.transform.GetChild(0).gameObject;
			furnitureObject.AddComponent<ItemSelection>();
			addTransformationManager(furnitureObject, furniture.mounting);
			MeshCollider meshCollider = furnitureObject.AddComponent<MeshCollider>();
			meshCollider.sharedMesh = furnitureObject.GetComponent<MeshFilter>().sharedMesh;
		}
	}

}

