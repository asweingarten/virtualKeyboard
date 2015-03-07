using UnityEngine;
using System.Collections;

public class DestroySelected : MonoBehaviour {

	private ItemManipulator itemManipulator;

	void Awake () {
		itemManipulator = (ItemManipulator)GameObject.FindObjectOfType ( typeof (ItemManipulator) );
		ArcMeshDrawer.OnRadialMenuActionTrigger += deleteSelected;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void deleteSelected(GameObject source) {
		if (source != gameObject) return;
		if( itemManipulator ) itemManipulator.deleteSelected();
	}
}
