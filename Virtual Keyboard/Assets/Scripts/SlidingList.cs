using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// [ExecuteInEditMode]
public class SlidingList : MonoBehaviour {

	GameObject addButton;
	GameObject upArrow;
	GameObject downArrow;
	GameObject leftArrow;
	GameObject rightArrow;
	GameObject categoryBox;

	int max_visible_elements;
	double scroll_distance;
	GameObject listObject;
	Vector3 baseScale;
	int categoryIndex = 0;

	int selectedIndex = 0;
	bool initialized = false;
	bool selector_at_top = true;

	// Use this for initialization
	void Start () {
		if (!initialized) {
			baseScale = gameObject.transform.localScale;
			
			Transform buttons = transform.Find ("buttons");
			
			addButton = buttons.Find ("AddButton").gameObject;
			upArrow = buttons.Find ("UpArrow").gameObject;
			downArrow = buttons.Find ("DownArrow").gameObject;
			leftArrow = buttons.Find ("LeftArrow").gameObject;
			rightArrow = buttons.Find ("RightArrow").gameObject;
			categoryBox = transform.Find ("TitleBoxFrame").gameObject;

			ListItem upArrowListItem = upArrow.GetComponent("ButtonItem") as ListItem;
			upArrowListItem.IsActive = true;
			ListItem downArrowListItem = downArrow.GetComponent("ButtonItem") as ListItem;
			downArrowListItem.IsActive = true;
			ListItem leftArrowListItem = leftArrow.GetComponent("ButtonItem") as ListItem;
			leftArrowListItem.IsActive = true;
			ListItem rightArrowListItem = rightArrow.GetComponent("ButtonItem") as ListItem;
			rightArrowListItem.IsActive = true;
			ListItem categoryBoxListItem = categoryBox.GetComponent("ButtonItem") as ListItem;
			categoryBoxListItem.IsActive = true;
			
			MenuItemSelection.OnMenuItemHold += HandleOnMenuItemHeld;
			MenuItemSelection.OnMenuItemGainedFocus += HandleOnMenuItemGainedFocus;
			MenuItemSelection.OnMenuItemLostFocus += HandleOnMenuItemLostFocus;
			selectedIndex = 0;
			categoryIndex = 0;
			
			initialized = true;
		}
		updateTransparency ();
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

	public void handleUpArrow() {
		slideListIncrement(1);
	}

	public void handleDownArrow() {
		slideListIncrement(-1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void callExecuteOnSelectedItem(){
		
	}

	public void createNewListItem(string text){
		baseScale = gameObject.transform.localScale;

		Transform listItemPrimitive = transform.Find ("ListItemPrimitive");
		TextMesh textMeshPrimitive = listItemPrimitive.Find("List_Item_Text").GetComponent<TextMesh>();

		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "ListItem";
		cube.transform.parent = transform.Find ("ListOfListOfItems").GetChild(categoryIndex).transform;
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

		// Debug.Log (transform.Find ("ListOfListOfItems").GetChild(selectedMultiIndex).childCount);
		cube.transform.Translate (new Vector3 (0, 0, (-0.12f * baseScale.z * (transform.Find ("ListOfListOfItems").GetChild(categoryIndex).childCount -2 - selectedIndex))));
		cube.AddComponent("FurnitureListItem");
		updateTransparency ();
	}

	void slideListIncrement(int i){
		baseScale = gameObject.transform.localScale;
		
		int newIndex = Mathf.Min(Mathf.Max (selectedIndex + i, 0), transform.Find ("ListOfListOfItems").GetChild(categoryIndex).childCount-2);

		Transform listOfItems = transform.Find ("ListOfListOfItems").GetChild(categoryIndex);
		listOfItems.Translate(new Vector3 (0, 0, (0.12f * baseScale.z * (newIndex - selectedIndex) )));

		Transform categoryBox = listOfItems.Find ("TitleBox");
		categoryBox.Translate(new Vector3 (0, 0, (-0.12f * baseScale.z * (newIndex - selectedIndex) )));

		selectedIndex = newIndex;

		updateTransparency ();
	}

	void updateTransparency (){
		Transform listOfItems = transform.Find ("ListOfListOfItems").GetChild (categoryIndex);
		for (int i = 0; i < listOfItems.childCount; i++) {
			Transform t = listOfItems.GetChild(i);
			if (t.name == "ListItem"){
				GameObject listItemBox = t.gameObject;
				TextMesh text = t.Find("List_Item_Text").gameObject.GetComponent<TextMesh>();
				Color color = listItemBox.renderer.material.color;
				Color text_color = text.renderer.material.color;
				
				listItemBox.SetActive(false);
				int diff = i - 1 - selectedIndex;
				ListItem listItem = listItemBox.GetComponent("FurnitureListItem") as ListItem;
				if (diff == 0){
					listItem.IsActive = true;
				} else {
					listItem.IsActive = false;
				}
				
				if (diff > -1 && diff < 7){
					listItemBox.SetActive(true);
				}
				
				listItemBox.renderer.material.color = color;
				text.renderer.material.color = text_color;
			}
		}
	}

	[ContextMenu ("Add New List Item")]
	void AddNewListItem () {
		Start ();
		createNewListItem(System.DateTime.Now.ToLongTimeString());
	}

	[ContextMenu ("Clear List")]
	void ClearList () {

		Debug.Log (categoryIndex);
		int childCount = transform.Find ("ListOfListOfItems").GetChild(categoryIndex).childCount;
		for (int i = childCount - 1; i >= 0; i--){
			if (transform.Find ("ListOfListOfItems").GetChild(categoryIndex).GetChild(i).name == "ListItem"){
				GameObject.DestroyImmediate(transform.Find ("ListOfListOfItems").GetChild(categoryIndex).GetChild(i).gameObject);
			}
		}
		selectedIndex = 0;
	}

	[ContextMenu ("Scroll Up")]
	void ScrollUp () {
		handleUpArrow();
	}

	[ContextMenu ("Scroll Down")]
	void ScrollDown () {
		handleDownArrow();
	}

	[ContextMenu ("Print All Items From GameObject")]
	void Debug1 () {
		Transform listOfItems = transform.Find ("ListOfListOfItems").GetChild(categoryIndex);
		Debug.Log (listOfItems);
		foreach (Transform t in listOfItems){
			Debug.Log (t.GetType());
			Transform textMeshTransform = t.Find("List_Item_Text");
			Debug.Log(textMeshTransform.gameObject.GetComponent<TextMesh>().text);
		}

	}



	public void handleLeftArrow() {
		multiListIncrement(-1);
	}
	
	public void handleRightArrow() {
		multiListIncrement(1);
	}
	
	void multiListIncrement(int i){
		slideListIncrement (0-selectedIndex);
		
		int newIndex = Mathf.Min(Mathf.Max (categoryIndex + i, 0), transform.Find ("ListOfListOfItems").childCount-1);
		Transform listOfItems = transform.Find ("ListOfListOfItems");
		
		int it = 0;
		foreach (Transform t in listOfItems) {
			if (it == newIndex){
				t.gameObject.SetActive(true);
			} else {
				t.gameObject.SetActive(false);
			}
			it++;
		}
		categoryIndex = newIndex;	
	}
	
	[ContextMenu ("Scroll Left")]
	void Scrollleft () {
		handleLeftArrow();
	}
	
	[ContextMenu ("Scroll Right")]
	void Scrollright () {
		handleRightArrow();
	}
} 
