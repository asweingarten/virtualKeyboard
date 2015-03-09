using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

	private GameObject selectedArc;

	public string interactionObjectName = "index";

	// Use this for initialization
	private List<GameObject> arcSections = new List<GameObject>();
	private List<Material> bodyMats;
	private List<Material> rimMats;

	private static int usageCount = 0;

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
		if (bodyMats == null || bodyMats.Count == 0) {
			Object[] bodyMatObjs = Resources.LoadAll ("RadialMenu/BodyMaterials");
			if( bodyMats == null ) {
				bodyMats = new List<Material>();
			}
		
			foreach (Object bodyMatObj in bodyMatObjs) {
				if( bodyMatObj is Material ) {
					Material bodyMat = (Material)bodyMatObj;
					bodyMats.Add( bodyMat );
				}
			}
		}

		if (rimMats == null || rimMats.Count == 0) {
			Object[] rimMatObjs = Resources.LoadAll ("RadialMenu/RimMaterials");
			if( rimMats == null ) {
				rimMats = new List<Material>();
			}

			foreach (Object rimMatObj in rimMatObjs) {
				if( rimMatObj is Material ) {
					Material rimMat = (Material)rimMatObj;
					rimMats.Add( rimMat );
				}
			}
		}
	}

	[ContextMenu("TestUsageCount")]
	void TestUsageCount() {
		Debug.Log ("Usage Count: " + usageCount);
		usageCount++;
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
		loadRadialMaterialResources ();
		if( bodyMats.Count > 0 ) {
			meshDrawer.arcBodyMaterial = bodyMats [usageCount % bodyMats.Count];
		}
		if( rimMats.Count > 0 ) {
			meshDrawer.arcRimMaterial = rimMats [usageCount % rimMats.Count];
		}
		usageCount++;
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
