using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SlidingList : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		SlidingListInteractionManager.OnNextCategory += nextCategory;
		SlidingListInteractionManager.OnPrevCategory += prevCategory;
		SlidingListInteractionManager.OnNextListItem += nextListItem;
		SlidingListInteractionManager.OnPrevListItem += prevListItem;
		SlidingListInteractionManager.OnCategoryListView += categoryListView; 
	}

	// Update is called once per frame
	void Update () {	
	}

	void OnDestroy() {
		SlidingListInteractionManager.OnNextCategory -= nextCategory;
		SlidingListInteractionManager.OnPrevCategory -= prevCategory;
		SlidingListInteractionManager.OnNextListItem -= nextListItem;
		SlidingListInteractionManager.OnPrevListItem -= prevListItem;
		SlidingListInteractionManager.OnCategoryListView -= categoryListView; 
	}

	void prevListItem() {
		//To Implement
	}

	void nextListItem() {
		//To Implement
	}

	void nextCategory() {
		//To Implement
	}
	
	void prevCategory() {
		//To Implement
	}

	void categoryListView() {
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
