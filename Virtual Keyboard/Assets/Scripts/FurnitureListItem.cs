using UnityEngine;
using System.Collections;

public class FurnitureListItem : ListItem
{
	public delegate void CreateFurniture(Furniture furniture);
	public static event CreateFurniture FurnitureCreated;

	public string furnitureName;
	public Furniture.Mounting mounting;
	public GameObject furnitureModel;

	public float scaleX = 1;
	public float scaleY = 1;
	public float scaleZ = 1;

	public float rotateX = 0;
	public float rotateY = 0;
	public float rotateZ = 0;
	
	Furniture createFurniture() {
		Vector3 scale = new Vector3(scaleX, scaleY, scaleZ);
		Vector3 rotation = new Vector3(rotateX, rotateY, rotateZ);
		return new Furniture(furnitureModel, mounting, scale, rotation);
	}

	public override void onItemChosen() {
		Debug.Log ("Create Furnitiure");
		FurnitureCreated(createFurniture());
	}
}

