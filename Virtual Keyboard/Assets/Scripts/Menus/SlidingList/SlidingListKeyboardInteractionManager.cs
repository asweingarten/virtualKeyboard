using UnityEngine;
using System.Collections;

public class SlidingListKeyboardInteractionManager : SlidingListInteractionManager {

	public string prevCategoryTrigger;
	public string nextCategoryTrigger;
	public string prevItemTrigger;
	public string nextItemTrigger;
	public string categoryListTrigger;
	public string selectionBoxTrigger;

	void Awake () {
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		string inputString = Input.inputString;
		foreach( char c in inputString ) {
			if (inputString.Contains(prevCategoryTrigger)) {
				prevCatagory();
			}
			if (inputString.Contains(nextCategoryTrigger)){
				nextCategory();
			}
			if (inputString.Contains(prevItemTrigger)) {
				prevListItem();
			}
			if (inputString.Contains(nextItemTrigger)) {
				nextListItem();
			}
			if (inputString.Contains(categoryListTrigger)) {
				categoryListView();
			}
			if (inputString.Contains(selectionBoxTrigger)) {
				selectionBoxChosen();
			}

		}

	}	
}
