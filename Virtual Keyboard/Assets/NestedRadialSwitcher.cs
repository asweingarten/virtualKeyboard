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
				if( !gameObject.tag.Contains("Back") ) {
					addBackOption();
				}
				ArcMeshDrawer.OnRadialMenuActionTrigger += switchMenu;
				child.SetActive(false);
				break;
			}
		}
	}

	public void addBackOption() {
		int childCount = nestedMenu.transform.childCount;
		for ( int i = 0; i < childCount; i++ ) {
			GameObject child = nestedMenu.transform.GetChild(i).gameObject;
			if( child.tag.Contains("Back") ) {
				backSection = child;
				return;
			}
		}
		RadialMenu radialMenu = nestedMenu.GetComponent<RadialMenu> ();
		backSection = radialMenu.createMenuOption ();
		ArcMeshDrawer arcMeshDrawer = backSection.GetComponent<ArcMeshDrawer> ();
		arcMeshDrawer.label = "Back";
		backSection.tag = "Back";
		backSection.AddComponent<NestedRadialSwitcher> ();
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy() {
		ArcMeshDrawer.OnRadialMenuActionTrigger -= switchMenu;
	}

	public void switchMenu( GameObject source ) {
		if(source != gameObject) return;

		//Create a copy of the current menu and assign as child of back button
		transform.root.gameObject.SetActive (false);
		if( backSection != null ) {
			GameObject currentMenuCopy = (GameObject)GameObject.Instantiate(transform.root.gameObject);
			currentMenuCopy.tag = "NestedMenu";
			currentMenuCopy.transform.parent = backSection.transform;
		}

		//Detach Child Menu and set it as active
		nestedMenu.transform.parent = null;
		nestedMenu.transform.position = transform.root.position;
		nestedMenu.transform.rotation = transform.root.rotation;
		nestedMenu.transform.localScale = transform.root.localScale;
		nestedMenu.SetActive (true);

		//Remove current menu
		if (transform.root.gameObject.tag != "RootMenu") {
			Destroy (transform.root.gameObject);
		} else {
			transform.root.gameObject.SetActive(false);
		}

	}
}
