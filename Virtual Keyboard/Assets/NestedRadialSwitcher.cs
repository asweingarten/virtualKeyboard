using UnityEngine;
using System.Collections;

public class NestedRadialSwitcher : MonoBehaviour {
	private GameObject nestedMenu;
	private GameObject backSection;

	// Use this for initialization
	void Start () {
		int childCount = gameObject.transform.childCount;
		for ( int i = 0; i < childCount; i++ ) {
			GameObject child = gameObject.transform.GetChild(i).gameObject;
			if( child.tag.Contains("NestedMenu") ) {
				nestedMenu = child;
				addBackOption();
				ArcMeshDrawer.OnRadialMenuActionTrigger += switchMenu;
				child.SetActive(false);
				break;
			}
		}
	}

	public void addBackOption() {

		RadialMenu radialMenu = nestedMenu.GetComponent<RadialMenu> ();
		backSection = radialMenu.createMenuOption ();
		ArcMeshDrawer arcMeshDrawer = backSection.GetComponent<ArcMeshDrawer> ();
		arcMeshDrawer.label = "Back";
		backSection.AddComponent<NestedRadialSwitcher> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void switchMenu( GameObject source ) {
		if(source != gameObject) return;

		Debug.Log ("switchMenu");
		//Create a copy of the current menu and assign as child of back button
		GameObject currentMenuCopy = (GameObject)GameObject.Instantiate(transform.root.gameObject);
		currentMenuCopy.SetActive (false);
		currentMenuCopy.transform.parent = backSection.transform;

		//Detach Child Menu and set it as active
		nestedMenu.transform.parent = null;
		nestedMenu.transform.position = transform.root.position;
		nestedMenu.transform.rotation = transform.root.rotation;
		nestedMenu.transform.localScale = transform.root.localScale;
		nestedMenu.SetActive (true);

		//Remove current menu
		//Destroy (transform.root.gameObject);


	}
}
