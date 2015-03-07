using UnityEngine;
using System.Collections;

public class ButtonItem : ListItem {

	public override void execute() {
		SlidingList sl = transform.root.GetComponent ("SlidingList") as SlidingList;
		switch (transform.name) {
		case "UpArrow":
			sl.handleUpArrow();
			break;
		case "DownArrow":
			sl.handleDownArrow();
			break;
		case "LeftArrow":
			sl.handleLeftArrow();
			break;
		case "RightArrow":
			sl.handleRightArrow();
			break;
		default:
			Debug.Log (transform.name);
			break;
		}
		// FurnitureCreated(createFurniture());
	}
}
