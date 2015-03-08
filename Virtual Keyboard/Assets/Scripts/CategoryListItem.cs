using UnityEngine;
using System.Collections;

public class CategoryListItem : ListItem {

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public override void execute() {
		SlidingList sl = transform.root.GetComponent ("SlidingList") as SlidingList;
		sl.handleCategorySelection ();
	}
}
