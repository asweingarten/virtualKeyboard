using UnityEngine;
using System.Collections;

using UnityEditor;
using UnityEngine;

using System.Collections.Generic;

public class SlidingListContextMenu : MonoBehaviour {
	
	// Add a menu item named "Do Something" to MyMenu in the menu bar.
	[ MenuItem( "GameObject/Create Other/List Item" ) ] 
	static void DoSomething () {
		// Transform slidingListTransform = transform.Find ("ListOfItems").parent;
		// SlidingList slidingList = slidingListTransform.GetComponent<SlidingList>();
		// slidingList.createNewListItem ("TEST");
		// Debug.Log ("Doing Something...");
	}
}