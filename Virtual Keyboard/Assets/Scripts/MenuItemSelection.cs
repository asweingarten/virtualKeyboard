using UnityEngine;
using System.Collections;

public class MenuItemSelection : MonoBehaviour {

	public delegate void MenuItemOnSelect(GameObject selectedItem);
	public static event MenuItemOnSelect OnMenuItemSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log(string.Format("Object {0} selected", gameObject.name));
		if (OnMenuItemSelected != null) {
			OnMenuItemSelected(gameObject);
		}
	}
}
