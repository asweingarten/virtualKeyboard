using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	}

	void nextListItem() {
		if( categoryManager != null ) {
			categoryManager.nextListItem ();
		}
	}

	void nextCategory() {
		if( categoryManager != null ) {
			categoryManager.nextCategory();
			updateTitleText();
		}
	}
	
	void prevCategory() {
		if( categoryManager != null ) {
			categoryManager.prevCategory();
			updateTitleText();
		}
	}

	void categoryListView() {
		if( categoryManager != null ) {
			categoryManager.displayCatagoryList();
			updateTitleText();
		}
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
} 
