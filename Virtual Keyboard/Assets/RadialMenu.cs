using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

	private GameObject selectedArc;

	// Use this for initialization
	private List<GameObject> arcSections = new List<GameObject>();
	void Start () {
		RadialMenuItemSelection.OnRadialMenuSelected += onArcSectionSelected;
		RadialMenuItemSelection.OnRadialMenuDeselected += onArcSectionDeselected;
		int numChildren = gameObject.transform.childCount;
		float totalWeight = 0f;
		//Calculate total weight of all arcSections and add them to a list
		for (int i = 0; i < numChildren; i++) {
			Transform childTransform = gameObject.transform.GetChild(i);
			GameObject child = childTransform.gameObject;
			//Look for ArcSections and make sure they are active
			if( child.name == "ArcSection" && child.activeSelf ) {
				arcSections.Add( child );
				totalWeight += child.GetComponent<ArcMeshDrawer>().arcWeight;
			}
		}

		//Keep track of starting rotation angle based on sum of prev arc lengths
		float prevRotation = 0;

		//Calculate the arcLength for each section based on section_weight/total_weight
		foreach( GameObject arcSection in arcSections ) {
			ArcMeshDrawer childArcDrawer = arcSection.GetComponent<ArcMeshDrawer>();
			float portion = childArcDrawer.arcWeight/totalWeight;
			childArcDrawer.arcLength = (2*Mathf.PI)*portion;
			childArcDrawer.createMeshes();
			arcSection.transform.rotation = Quaternion.identity;
			arcSection.transform.Rotate(new Vector3( 0f, 0f, Mathf.Rad2Deg*prevRotation));
			prevRotation += childArcDrawer.arcLength;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onArcSectionSelected(GameObject selected) {
		if (selectedArc != null)
			return;
		selectedArc = selected.transform.parent.gameObject;
		selectedArc.GetComponent<ArcMeshDrawer> ().selectSection ();
	}

	void onArcSectionDeselected(GameObject selected) {
		if (selectedArc == null)
			return;
		selectedArc.GetComponent<ArcMeshDrawer> ().deselectSection ();
		selectedArc = null;
	}


}
