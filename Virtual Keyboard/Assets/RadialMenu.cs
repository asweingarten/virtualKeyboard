using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

	private GameObject selectedArc;

	public string interactionObjectName = "index";

	// Use this for initialization
	private List<GameObject> arcSections = new List<GameObject>();
	void Start () {
		RadialMenuItemSelection.OnRadialMenuSelected += onArcSectionSelected;
		RadialMenuItemSelection.OnRadialMenuDeselected += onArcSectionDeselected;
		updateArcSectionProperties ();
	}

	void updateArcSectionProperties() {
		int numChildren = gameObject.transform.childCount;
		float totalWeight = 0f;
		//Calculate total weight of all arcSections and add them to a list
		arcSections.Clear ();
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
			childArcDrawer.selectionObjectName = interactionObjectName;
			float portion = childArcDrawer.arcWeight/totalWeight;

			childArcDrawer.createMeshes();

			arcSection.transform.localRotation = Quaternion.Euler( 0f, 0f, Mathf.Rad2Deg*prevRotation);
			childArcDrawer.arcLength = (2*Mathf.PI)*portion;
			prevRotation += childArcDrawer.arcLength;
		}
	}
	
	[ContextMenu("UndrawRadialMenu")]
	void UndrawRadialMenu() {
		int numChildren = gameObject.transform.childCount;
		//Calculate total weight of all arcSections and add them to a list
		for (int i = 0; i < numChildren; i++) {
			Transform childTransform = gameObject.transform.GetChild(i);
			GameObject child = childTransform.gameObject;
			//Look for ArcSections and make sure they are active
			if( child.name == "ArcSection" && child.activeSelf ) {
				ArcMeshDrawer childArcDrawer = child.GetComponent<ArcMeshDrawer>();
				childArcDrawer.removeComponents();
				child.transform.rotation = Quaternion.identity;
				child.transform.localPosition = Vector3.zero;
				child.transform.localScale = Vector3.one;
			}
		}

	}

	[ContextMenu("DrawRadialMenu")]
	void DrawRadialMenu() {
		UndrawRadialMenu ();
		arcSections.Clear ();
		int numChildren = gameObject.transform.childCount;
		//Calculate total weight of all arcSections and add them to a list
		for (int i = 0; i < numChildren; i++) {
			Transform childTransform = gameObject.transform.GetChild(i);
			GameObject child = childTransform.gameObject;
			//Look for ArcSections and make sure they are active
			if( child.name == "ArcSection" && child.activeSelf ) {
				arcSections.Add( child );
			}
		}

		foreach (GameObject arcSection in arcSections) {
			ArcMeshDrawer childArcDrawer = arcSection.GetComponent<ArcMeshDrawer>();
			childArcDrawer.createComponentsIfNeeded();//Trigger the creation of the sub objects if needed;
		}
		updateArcSectionProperties ();
	}

	private void loadRadialMaterialResources() {
		Object[] bodyMats = Resources.LoadAll ("RadialMenu/BodyMaterials");
		Object[] rimMats = Resources.LoadAll ("RadialMenu/RimMaterials");

	}

	[ContextMenu("AddMenuOption")]
	void AddMenuOption() {
		createMenuOption ();
	}

	public GameObject createMenuOption() {
		GameObject menuOption = new GameObject();
		menuOption.name = "ArcSection";
		ArcMeshDrawer meshDrawer = menuOption.AddComponent<ArcMeshDrawer> ();
		menuOption.transform.parent = gameObject.transform;
		meshDrawer.arcBodyMaterial = Resources.Load("RadialMenu/BlockMat2", typeof(Material)) as Material;
		meshDrawer.arcRimMaterial  = Resources.Load("RadialMenu/BlackReflectiveMat", typeof(Material)) as Material;
		meshDrawer.arcWeight = 1;
		menuOption.transform.localPosition = Vector3.zero;
		menuOption.transform.localScale = Vector3.one;
		return menuOption;
	}

	void OnDestroy() {
		RadialMenuItemSelection.OnRadialMenuSelected -= onArcSectionSelected;
		RadialMenuItemSelection.OnRadialMenuDeselected -= onArcSectionDeselected;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void onArcSectionSelected(GameObject selected) {
		if (selectedArc != null) return;
		if (selected == null ||
		    selected.transform.parent == null || 
		    selected.transform.parent.parent == null || 
		    selected.transform.parent.parent.gameObject != gameObject ) return;
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
