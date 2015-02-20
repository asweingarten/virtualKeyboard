using UnityEngine;
using System.Collections;

public class RadialMenuItemSelection : MonoBehaviour {
	
	public delegate void RadialMenuItemOnSelect(GameObject selectedItem);
	public delegate void RadialMenuItemOnDeselect(GameObject selectedItem);
	public delegate void RadialMenuOnSelect(GameObject selectedItem);
	public delegate void RadialMenuOnDeselect(GameObject selectedItem);
	public static event RadialMenuItemOnSelect OnRadialMenuItemSelected;
	public static event RadialMenuItemOnDeselect OnRadialMenuItemDeselected;
	public static event RadialMenuOnSelect OnRadialMenuSelected;
	public static event RadialMenuOnDeselect OnRadialMenuDeselected;

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
		if (selectedObject == gameObject) {	// Do not fire the event if the selected object is equal to the current game object.
			return;
		}
		
		selectedObject = gameObject;
		if (OnRadialMenuSelected != null) {
			OnRadialMenuSelected(gameObject);
		}
	}
	
	void OnCollisionExit(Collision collision) {
		if (selectedObject && OnRadialMenuDeselected != null) {
			OnRadialMenuDeselected(gameObject);
			selectedObject = null;
		}
	}
}
