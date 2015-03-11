using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CategoryManager : MonoBehaviour {

	private float listItemHeight = 0.1f;
	private float listItemSpacing = 0.05f;
	private List<GameObject> categories;
	private GameObject categoryList;

	private int currentCategory = 0;

	void Awake () {
		categories = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () {
		findCategories ();
		if( categories.Count > 1 ) {
			createCategoryList ();
		} else if( categories.Count == 1 ) {
			categories[0].SetActive(true);
		} else {
			//If list has no entries destroy it
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void createCategoryListEntries() {
		int itemCount = 0;
		foreach( GameObject category in categories ) {
			GameObject categoryEntry = GameObject.CreatePrimitive(PrimitiveType.Cube);
			categoryEntry.name = category.name;//Name of entry inheirits from category name
			categoryEntry.transform.SetParent( categoryList.transform );//CategoryEntry is child of category list
			categoryEntry.tag = "ListItem";
			Transform entryTransform = categoryEntry.transform;

			//Note: Positioning of these elements is handled In ListManager component
			entryTransform.localScale = new Vector3( 1f, 1f, listItemHeight );
			entryTransform.localRotation = Quaternion.Euler(Vector3.zero);

			GameObject categoryText = new GameObject();
			categoryText.transform.SetParent(categoryEntry.transform);
			TextMesh textMesh = categoryText.AddComponent<TextMesh>();
			textMesh.text = category.name;
			textMesh.characterSize = 0.045f;
			textMesh.fontSize = 20;
			textMesh.anchor = TextAnchor.MiddleCenter;
			textMesh.font = textMesh.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			categoryText.GetComponent<MeshRenderer>().material = textMesh.font.material;
			categoryText.transform.localPosition = Vector3.zero;
			categoryText.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
			categoryText.transform.localScale = new Vector3( 1f, 10f, 1f );

			++itemCount;
		}
	}

	private bool isCategoryListCurrent () {
		if( categoryList == null ) return false;
		List<string> categoryNames = new List<string> ();
		foreach( GameObject category in categories ) {
			categoryNames.Add(category.name);
		}
		int childCount = categoryList.transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject categoryEntry = categoryList.transform.GetChild(i).gameObject;
			if(categoryNames.Contains(categoryEntry.name)) {
				//Remove matching category name
				categoryNames.Remove(categoryEntry.name);
			} else {
				//If our categoryList object has entries for non exisitent categories it is not up to date
				return false;
			}
		}
		//Our category object is current if all categories names have been removed
		return categoryNames.Count == 0;
	}

	private void createCategoryList() {
		if( isCategoryListCurrent() ) return;//update do not create if exisiting category list is up to date
		if( categoryList != null ) Destroy(categoryList);//Destroy previous categoryList if it exists

		categoryList = new GameObject ();
		categoryList.transform.SetParent (transform);
		categoryList.name = "CategoryList";
		categoryList.tag = "CategoryList";
		categoryList.transform.localScale = Vector3.one;
		categoryList.transform.localPosition = Vector3.zero;
		categoryList.transform.localRotation = Quaternion.Euler (Vector3.zero); 
		createCategoryListEntries ();

		//Add Category Component to handle list item positioning and scrolling
		ListManager listManager = categoryList.AddComponent<ListManager> ();
		listManager.title = "CategoryList";
	}

	//Initialize list of categories and find categoryList
	private void findCategories() {
		int childCount = transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject child = transform.GetChild(i).gameObject;
			Debug.Log(child.name);
			if( child.tag.Equals("Category") ) {
				categories.Add(child);
				child.SetActive(false);//Start with all categories inactive
			} else if ( child.tag.Equals("CategoryList") ) {
				Debug.Log ("Found Category List");
				categoryList = child;
			}
		}
	}

	void updateCategoryList() {
	}

	private void deactivateAllCategories() {
		foreach( GameObject category in categories ) {
			category.SetActive(false);
		}
	}

	//Assumes categoryList not null and categories not empty
	public void returnToLastActiveCategory() {
		categoryList.SetActive(false);
		categories [currentCategory].SetActive (true);
	}

	public void nextCategory() {
		//In the event categoryList is active return to last active category
		if (categoryList != null && categoryList.activeSelf == true) {
			returnToLastActiveCategory();
		} else {
			categories [currentCategory].SetActive (false);
			currentCategory = (currentCategory + 1) % categories.Count;
			categories [currentCategory].SetActive (true);
		}

	}

	public void prevCategory() {
		//In the event categoryList is active return to last active category
		if (categoryList != null && categoryList.activeSelf == true) {
			returnToLastActiveCategory();
		} else {
			categories [currentCategory].SetActive (false);
			currentCategory = (currentCategory - 1) % categories.Count;
			categories [currentCategory].SetActive (true);
		}

	}

	public void displayCatagoryList() {
		categories [currentCategory].SetActive (false);
		categoryList.SetActive (true);
	}

	public void prevListItem () {
		ListManager listManager = categories [currentCategory].GetComponent<ListManager> ();
		if( listManager != null ) {
			listManager.prevListItem();
		}
	}

	public void nextListItem () {
		ListManager listManager = categories [currentCategory].GetComponent<ListManager> ();
		if( listManager != null ) {
			listManager.nextListItem();
		}
	}

}
