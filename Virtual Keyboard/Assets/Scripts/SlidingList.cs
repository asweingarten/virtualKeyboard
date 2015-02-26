using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [ExecuteInEditMode]
public class SlidingList : MonoBehaviour {

	class InnerListItem {
		public Object objectContained;
		public Transform boxTransform;
		public TextMesh itemText;
		public Object image;
	}

	List<InnerListItem> items;

	GameObject addButton;
	GameObject upArrow;
	GameObject downArrow;

	int max_visible_elements;
	double scroll_distance;
	GameObject listObject;
	Vector3 baseScale;

	int selectedIndex;
	bool initialized = false;
	bool selector_at_top = true;

	// Use this for initialization
	void Start () {
		myInitialize ();
	}

	void myInitialize(){
		if (!initialized) {
			baseScale = transform.Find ("ListOfItems").parent.transform.localScale;
			
			items = new List<InnerListItem> ();
			
			Transform buttons = transform.Find ("buttons");
			
			addButton = buttons.Find ("AddButton").gameObject;
			upArrow = buttons.Find ("UpArrow").gameObject;
			downArrow = buttons.Find ("DownArrow").gameObject;
			
			MenuItemSelection.OnMenuItemHold += HandleOnMenuItemHeld;
			MenuItemSelection.OnMenuItemGainedFocus += HandleOnMenuItemGainedFocus;
			MenuItemSelection.OnMenuItemLostFocus += HandleOnMenuItemLostFocus;
			selectedIndex = 0;

			initialized = true;
		}
	}

	void HandleOnMenuItemLostFocus (GameObject selectedItem)
	{
		if (selectedItem == addButton || selectedItem == upArrow || selectedItem == downArrow) {
			GameObject listItemText = selectedItem.transform.Find ("List_Item_Text").gameObject;
			TextMesh textMesh = listItemText.GetComponent<TextMesh>();
			textMesh.color = Color.blue;
		}
	}

	void HandleOnMenuItemGainedFocus (GameObject selectedItem)
	{
		if (selectedItem == addButton || selectedItem == upArrow || selectedItem == downArrow) {
			GameObject listItemText = selectedItem.transform.Find ("List_Item_Text").gameObject;
			TextMesh textMesh = listItemText.GetComponent<TextMesh>();
			textMesh.color = Color.yellow;
		}
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
		slideListIncrement(-1);
	}

	void handleDownArrow() {
		slideListIncrement(-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createNewListItem(string text){
		InnerListItem newInnerListItem = new InnerListItem();
		Transform listItemPrimitive = transform.Find ("ListItemPrimitive");
		TextMesh textMeshPrimitive = listItemPrimitive.Find("List_Item_Text").GetComponent<TextMesh>();

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "ListItem";
		cube.transform.parent = transform.Find ("ListOfItems").transform;
		cube.transform.localScale = listItemPrimitive.localScale;
		cube.transform.position = listItemPrimitive.position;
		cube.transform.rotation = listItemPrimitive.rotation;
		cube.renderer.material = listItemPrimitive.renderer.material;
		
		TextMesh txtMesh = (TextMesh) TextMesh.Instantiate(textMeshPrimitive);
		txtMesh.name = "List_Item_Text";
		txtMesh.text = text;
		txtMesh.transform.parent = cube.transform;
		txtMesh.transform.localScale = textMeshPrimitive.transform.localScale;
		txtMesh.transform.position = textMeshPrimitive.transform.position;
		txtMesh.transform.rotation = textMeshPrimitive.transform.rotation;

		cube.transform.Translate (new Vector3 (0, 0, (-0.12f * baseScale.z * items.Count )));

		newInnerListItem.boxTransform = cube.transform;
		newInnerListItem.itemText = txtMesh;
		items.Add (newInnerListItem);
		
		updateTransparency ();
	}

	void slideListIncrement(int i){
		int newIndex = Mathf.Min(Mathf.Max (selectedIndex + i, 0), items.Count-1);

		Transform listOfItems = transform.Find ("ListOfItems");
		listOfItems.Translate(new Vector3 (0, 0, (0.12f * baseScale.z * (newIndex - selectedIndex) )));
		selectedIndex = newIndex;
		Debug.Log (newIndex);

		updateTransparency ();
	}

	void updateTransparency (){
		int index = 0;
		foreach(InnerListItem item in items){
			GameObject cube = item.boxTransform.gameObject;
			TextMesh text = item.itemText;
			Color color = cube.renderer.material.color;
			Color text_color = text.renderer.material.color;

			if (selector_at_top){
				// THIS CODE IS FOR WHEN SELECTOR IS THE TOP ITEM OF A 7 ITEM LIST
				color.a = 0.0f;
				text_color.a = 0.0f;
				int diff = index - selectedIndex;
				if (diff == -1 || diff == 7){
					color.a = 0.5f;
					text_color.a = 0.5f;
				} else if (diff > -1 && diff < 7){
					color.a = 1.0f;
					text_color.a = 1.0f;
				}
			} else {
				// THIS CODE IS FOR WHEN SELECTOR IS THE CENTER ITEM OF A 7 ITEM LIST
				if (Mathf.Abs (index - selectedIndex) > 4){
					color.a = 0.0f;
					text_color.a = 0.0f;
				} else if (Mathf.Abs (index - selectedIndex) == 4){
					color.a = 0.5f;
					text_color.a = 0.5f;
				} else if (Mathf.Abs (index - selectedIndex) < 4){
					color.a = 1.0f;
					text_color.a = 1.0f;
				}
			}

			cube.renderer.material.color = color;
			text.renderer.material.color = text_color;
			index++;
		}
	}

	[ContextMenu ("Add New List Item")]
	void DoSomething2 () {
		myInitialize ();
		createNewListItem(System.DateTime.Now.ToLongTimeString());
	}
} 
