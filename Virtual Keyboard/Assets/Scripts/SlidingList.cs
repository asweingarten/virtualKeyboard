using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlidingList : MonoBehaviour {

	class InnerListItem{
		Object object_conntained;
		Transform transform;
		string item_text;
		Object image;
	}

	List<ListItem> items;
	List<Transform> transformList;
	double height_of_item;
	double height_of_space;
	GameObject listObject;

	Vector3 base_scale;

	int max_visible_elements;
	double scroll_distance;

	// Use this for initialization
	void Start () {
		max_visible_elements = 7;
		scroll_distance = 0d;
		transformList = new List<Transform> ();
		Transform listOfItems = transform.Find ("ListOfItems");
		base_scale = listOfItems.parent.transform.localScale;
		listObject = listOfItems.gameObject;
		if (listOfItems) {
			foreach (Transform item in listOfItems){
				TextMesh textMesh = item.Find("List_Item_Text").GetComponent<TextMesh>();

				transformList.Add (item);
			}
		}

		Debug.Log(transformList.Count);

		createNewListItem ("blahblah");
		createNewListItem ("blahblah 2");
		createNewListItem ("blahblah 3");
		slide_list_increment (-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createNewListItem(string text){
		Transform lastElement = transformList[transformList.Count - 1];
		TextMesh lastTextMesh = lastElement.Find("List_Item_Text").GetComponent<TextMesh>();

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = listObject.transform;
		cube.transform.localScale = lastElement.localScale;
		cube.transform.position = lastElement.position;
		cube.transform.rotation = lastElement.rotation;


		TextMesh txtMesh = (TextMesh) TextMesh.Instantiate(lastTextMesh);
		txtMesh.transform.parent = cube.transform;

		txtMesh.transform.localScale = lastTextMesh.transform.localScale;
		txtMesh.transform.position = lastTextMesh.transform.position;
		txtMesh.transform.rotation = lastTextMesh.transform.rotation;

		txtMesh.text = text;

		cube.transform.Translate (new Vector3 (0, 0, (-0.12f * base_scale.z )));

		cube.name = "ListItem";
		txtMesh.name = "List_Item_Text";

		transformList.Add(cube.transform);	
	}

	void slide_list_increment(int i){
		Transform firstElement = transformList[0];
		Transform listOfItems = firstElement.parent.transform;
		listOfItems.Translate(new Vector3 (0, 0, (-0.12f * base_scale.z * i )));

		scroll_distance = scroll_distance + i;
	}
} 
