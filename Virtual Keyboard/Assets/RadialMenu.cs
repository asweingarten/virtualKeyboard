using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {
	
	// Use this for initialization
	private List<GameObject> arcSections = new List<GameObject>();
	void Start () {
		int numChildren = gameObject.transform.childCount;
		float totalWeight = 0f;
		for (int i = 0; i < numChildren; i++) {
			Transform childTransform = gameObject.transform.GetChild(i);
			GameObject child = childTransform.gameObject;
			if( child.name == "ArcSection" && child.activeSelf ) {
				arcSections.Add( child );
				ArcMeshDrawer childArcDrawer = child.GetComponent<ArcMeshDrawer>();
				totalWeight += childArcDrawer.arcWeight;
			}
		}

		float prevArcLength = 0;
		foreach( GameObject arcSection in arcSections ) {
			ArcMeshDrawer childArcDrawer = arcSection.GetComponent<ArcMeshDrawer>();
			float portion = childArcDrawer.arcWeight/totalWeight;
			Debug.Log ("Total weight: " + totalWeight + ", arcWeight: " + childArcDrawer.arcWeight + ", portion: " + portion);
			Debug.Log ("ArcLength: " + (2*Mathf.PI)*portion);
			childArcDrawer.arcLength = (2*Mathf.PI)*portion;
			childArcDrawer.createMeshes();
			arcSection.transform.rotation = Quaternion.identity;
			arcSection.transform.Rotate(new Vector3( 0f, 0f, Mathf.Rad2Deg*prevArcLength));
			prevArcLength += childArcDrawer.arcLength;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
