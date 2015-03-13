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
		SlidingListInteractionManager.OnSelectionBoxChosen += chooseActiveItem; 
		categoryManager = gameObject.GetComponentInChildren<CategoryManager> ();
		updateTitleText ();
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
		SlidingListInteractionManager.OnSelectionBoxChosen -= chooseActiveItem; 
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
			categoryManager.nextCategory();
			updateTitleText();
		}
		//To Implement
	}
	
	void prevCategory() {
		if( categoryManager != null ) {
			categoryManager.prevCategory();
			updateTitleText();
		}
		//To Implement
	}

	void categoryListView() {
		if( categoryManager != null ) {
			categoryManager.displayCatagoryList();
			updateTitleText();
		}
		//To Implement
	}

	void chooseActiveItem() {
		if( categoryManager != null ) {
			categoryManager.chooseActiveItem();
			updateTitleText();
		}
	}

	public void updateTitleText() {
		ListManager listManager = categoryManager.getActiveCategory();
		if( listManager != null ) {
			CategoryTitle[] titleComponents = gameObject.GetComponentsInChildren<CategoryTitle> ();
			foreach( CategoryTitle component in titleComponents ) {
				component.updateTitleText(listManager.title);
			}
		}
	}

	[ContextMenu ("Add New Category")]
	void AddNewCategory () {
		//To Implement
	}
} 
