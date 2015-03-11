using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}

	// Update is called once per frame
	void Update () {	
	}

	public void prevListItem() {
		//To Implement
	}

	public void nextListItem() {
		//To Implement
	}

	public void nextCategory() {
		//To Implement
	}
	
	public void prevCategory() {
		//To Implement
	}


	public void createNewListItem(string text){
		//To Implement
	}

	[ContextMenu ("Add New List Item")]
	void AddNewListItem () {
		//To implement
	}

	[ContextMenu ("Add New Category")]
	void AddNewCategory () {
		//To Implement
	}
} 
