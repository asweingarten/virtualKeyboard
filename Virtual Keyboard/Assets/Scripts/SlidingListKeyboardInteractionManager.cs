using UnityEngine;
using System.Collections;

public class SlidingListKeyboardInteractionManager : SlidingListInteractionManager {

	public string prevCategoryTrigger;
	public string nextCategoryTrigger;
	public string prevItemTrigger;
	public string nextItemTrigger;
	public string categoryListTrigger;
	public string selectionBoxTrigger;

	private char activeCharacter;

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
				activeCharacter = c;
				prevCatagory();
			}
			if (inputString.Contains(nextCategoryTrigger)){
				activeCharacter = c;
				nextCategory();
			}
			if (inputString.Contains(prevItemTrigger)) {
				activeCharacter = c;
				prevListItem();
			}
			if (inputString.Contains(nextItemTrigger)) {
				activeCharacter = c;
				nextListItem();
			}
			if (inputString.Contains(categoryListTrigger)) {
				activeCharacter = c;
				categoryListView();
			}
			if (inputString.Contains(selectionBoxTrigger)) {
				activeCharacter = c;
				selectionBoxChosen();
			}

		}

	}	
}
