using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SlidingList : MonoBehaviour {

	private CategoryManager categoryManager;
	// Use this for initialization
	void Start () {
		SlidingListInteractionManager.OnNextCategory += nextCategory;
		SlidingListInteractionManager.OnPrevCategory += prevCategory;
		SlidingListInteractionManager.OnNextListItem += nextListItem;
		SlidingListInteractionManager.OnPrevListItem += prevListItem;
		SlidingListInteractionManager.OnCategoryListView += categoryListView; 

		categoryManager = gameObject.GetComponentInChildren<CategoryManager> ();
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
		if( categoryManager != null ) {
			categoryManager.prevListItem();
		}
		//To Implement
	}

	void nextListItem() {
		if( categoryManager != null ) {
			categoryManager.nextListItem ();
		}
		//To Implement
	}

	void nextCategory() {
		if( categoryManager != null ) {
			string newCategory = categoryManager.nextCategory();
			updateTitleText(newCategory);
		}
		//To Implement
	}
	
	void prevCategory() {
		if( categoryManager != null ) {
			string newCategory = categoryManager.prevCategory();
		}
		//To Implement
	}

	void categoryListView() {
		if( categoryManager != null ) {
			categoryManager.displayCatagoryList();
			if( categoryManager.isDisplayingCategoryList() ) {
				updateTitleText("Categories");
			}
		}
		//To Implement
	}

	void updateTitleText(string title) {
		CategoryTitle[] titleComponents = gameObject.GetComponentsInChildren<CategoryTitle> ();
		foreach( CategoryTitle component in titleComponents ) {
			component.updateTitleText(title);
		}
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
