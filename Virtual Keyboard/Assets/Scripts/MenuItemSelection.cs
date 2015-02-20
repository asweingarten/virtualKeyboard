using UnityEngine;
using System.Collections;

public class MenuItemSelection : MonoBehaviour {

	public delegate void MenuItemOnSelect(GameObject selectedItem);
	public static event MenuItemOnSelect OnMenuItemSelected;

	private static object selectedObjectLock = new object();
	private static GameObject globallySelectedObject;

	public static GameObject selectedObject {
		get {
			return globallySelectedObject;
		}
		set {
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
		if (OnMenuItemSelected != null) {
			OnMenuItemSelected(gameObject);
		}
	}
}
