using UnityEngine;
using System.Collections;

public class RadialMenuItemSelection : MonoBehaviour {
	

	public delegate void RadialMenuOnSelect(GameObject selectedItem);
	public delegate void RadialMenuOnDeselect(GameObject selectedItem);
	public static event RadialMenuOnSelect OnRadialMenuSelected;
	public static event RadialMenuOnDeselect OnRadialMenuDeselected;

	public string selectionObjectName { get; set;}

	private static object selectedObjectLock = new object();
	private static GameObject globallySelectedObject;
	
	public static GameObject selectedObject {
		get {
			return globallySelectedObject;
		}
		private set {
			lock (selectedObjectLock) {
				globallySelectedObject = value;
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision) {
		GameObject collidingObject = collision.gameObject;
		if( selectionObjectName != null && selectionObjectName.Length != 0 && collidingObject.name != selectionObjectName ) {
			Transform parentTransform = collidingObject.transform.parent;
			if( parentTransform == null || parentTransform.gameObject.name != selectionObjectName ) {
				return;
			}
		}

		//Do not fire action more than once per distinct selection
		if (selectedObject == gameObject) {
			return;
		}
		
		selectedObject = gameObject;
		if (OnRadialMenuSelected != null) {
			OnRadialMenuSelected(gameObject);
		}
	}
	
	void OnCollisionExit(Collision collision) {
		GameObject collidingObject = collision.gameObject;
		if( selectionObjectName != null && selectionObjectName.Length != 0 && collidingObject.name != selectionObjectName ) {
			Transform parentTransform = collidingObject.transform.parent;
			if( parentTransform == null || parentTransform.gameObject.name != selectionObjectName ) {
				return;
			}
		}

		//Only deselect event if this object is the currently selected one
		if (selectedObject && OnRadialMenuDeselected != null) {
			OnRadialMenuDeselected(gameObject);
			selectedObject = null;
		}
	}
}
