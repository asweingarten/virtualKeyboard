using UnityEngine;
using System.Collections;

public class RadialActionFireDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ArcMeshDrawer.OnRadialMenuActionTrigger += debugMessage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void debugMessage(GameObject selected) {
		if (selected == gameObject) {
			Debug.Log ("Arc section selected");
		}
	}
}
