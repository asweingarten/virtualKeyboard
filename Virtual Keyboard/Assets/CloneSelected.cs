using UnityEngine;
using System.Collections;

public class CloneSelected : MonoBehaviour {

	private ItemManipulator itemManipulator;
	
	void Awake () {
		itemManipulator = (ItemManipulator)GameObject.FindObjectOfType ( typeof (ItemManipulator) );
		ArcMeshDrawer.OnRadialMenuActionTrigger += cloneSelected;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		ArcMeshDrawer.OnRadialMenuActionTrigger -= cloneSelected;
	}

	private void cloneSelected(GameObject source) {
		if (source != gameObject) return;
		if( itemManipulator ) itemManipulator.cloneSelected();
	}
}
