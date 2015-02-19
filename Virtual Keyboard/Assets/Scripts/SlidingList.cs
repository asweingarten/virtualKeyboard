using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlidingList : MonoBehaviour {

	List<ListItem> items;
	List<Transform> transformtList;
	double height_of_item;
	double height_of_space;
	GameObject listObject;

	// Use this for initialization
	void Start () {
		transformtList = new List<Transform> ();
		Transform listOfItems = transform.Find ("ListOfItems");
		listObject = listOfItems.gameObject;
		if (listOfItems) {
			foreach (Transform item in listOfItems){
				TextMesh textMesh = item.Find("List_Item_Text").GetComponent<TextMesh>();

				transformtList.Add (item);
			}
		}

		Debug.Log(transformtList.Count);

		createNewListItem ("blahblah");
		createNewListItem ("blahblah 2");
		createNewListItem ("blahblah 3");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createNewListItem(string text){
		Transform lastElement = transformtList[transformtList.Count - 1];
		TextMesh lastTextMesh = lastElement.Find("List_Item_Text").GetComponent<TextMesh>();

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = listObject.transform;
		cube.transform.localScale = lastElement.localScale;
		cube.transform.position = lastElement.position;


		TextMesh txtMesh = (TextMesh) TextMesh.Instantiate(lastTextMesh);
		txtMesh.transform.parent = cube.transform;

		txtMesh.transform.localScale = lastTextMesh.transform.localScale;
		txtMesh.transform.position = lastTextMesh.transform.position;
		txtMesh.transform.rotation = lastTextMesh.transform.rotation;
		// txtMesh.transform.Translate (new Vector3 (0, 0, -0.12f));

		txtMesh.text = text;

		cube.transform.Translate (new Vector3 (0, 0, -0.12f));

		cube.name = "ListItem";
		txtMesh.name = "List_Item_Text";

		transformtList.Add(cube.transform);

		
	}
}
