using UnityEngine;
using System.Collections;

public class DebugListItem : ListItem
{		
	public override void onItemChosen() {
		Debug.Log ("ListItem Chosen: " + itemText);
	}
}
