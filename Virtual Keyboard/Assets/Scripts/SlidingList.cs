using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlidingList : MonoBehaviour {

	class InnerListItem{
		public Object object_contained;
		public Transform box_transform;
		public string item_text;
		public Object image;
	}

	List<InnerListItem> items;

	GameObject addButton;
	GameObject upArrow;
	GameObject downArrow;

	Vector3 base_scale;
	int max_visible_elements;
	double scroll_distance;

	// Use this for initialization
	void Start () {
		max_visible_elements = 7;
		scroll_distance = 0d;

		Transform listOfItems = transform.Find ("ListOfItems");
		base_scale = listOfItems.parent.transform.localScale;

		items = new List<InnerListItem> ();
		
		addButton = transform.Find ("AddButton").gameObject;
		upArrow = transform.Find ("UpArrow").gameObject;
		downArrow = transform.Find ("DownArrow").gameObject;
		
		MenuItemSelection.OnMenuItemHold += HandleOnMenuItemHeld;

		Debug.Log(transformList.Count);

		createNewListItem ("blahblah");
		createNewListItem ("blahblah 2");
		createNewListItem ("blahblah 3");
		slide_list_increment (-2);
	}

	void HandleOnMenuItemHeld (GameObject selectedItem)
	{
		if (selectedItem == addButton) {
			handleAddButton ();
		} else if (selectedItem == upArrow) {
			handleUpArrow ();
		} else if (selectedItem == downArrow) {
			handleDownArrow ();
		}
	}

	void handleAddButton() {
		createNewListItem(System.DateTime.Now.ToLongTimeString());
	}

	void handleUpArrow() {
		slide_list_increment(1);
	}

	void handleDownArrow() {
		slide_list_increment(-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createNewListItem(string text){
		InnerListItem new_inner_list_item = new InnerListItem();
		Transform listItemPrimitive = transform.Find ("ListItemPrimitive");
		Transform listOfItems = transform.Find ("ListOfItems");
		TextMesh textMeshPrimitive = listItemPrimitive.Find("List_Item_Text").GetComponent<TextMesh>();

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.parent = listOfItems.transform;
		cube.transform.localScale = listItemPrimitive.localScale;
		cube.transform.position = listItemPrimitive.position;
		cube.transform.rotation = listItemPrimitive.rotation;


		TextMesh txtMesh = (TextMesh) TextMesh.Instantiate(textMeshPrimitive);
		txtMesh.transform.parent = cube.transform;

		txtMesh.transform.localScale = textMeshPrimitive.transform.localScale;
		txtMesh.transform.position = textMeshPrimitive.transform.position;
		txtMesh.transform.rotation = textMeshPrimitive.transform.rotation;

		txtMesh.text = text;

		cube.transform.Translate (new Vector3 (0, 0, (-0.12f * base_scale.z * items.Count )));

		cube.name = "ListItem";
		txtMesh.name = "List_Item_Text";

		new_inner_list_item.box_transform = cube.transform;
		new_inner_list_item.item_text = text;

		items.Add (new_inner_list_item);
	}

	void slide_list_increment(int i){
		Transform listOfItems = transform.Find ("ListOfItems");
		listOfItems.Translate(new Vector3 (0, 0, (0.12f * base_scale.z * i )));

		scroll_distance = scroll_distance + i;
	}
} 
