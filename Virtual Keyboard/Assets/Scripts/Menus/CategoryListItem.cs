using UnityEngine;
using System.Collections;

public class CategoryListItem : ListItem {

	public CategoryManager categoryManager;
	public GameObject category;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void onItemChosen ()
	{
		if( categoryManager != null && category != null ) {
			categoryManager.displayCategory(category);
		}
	}
}
