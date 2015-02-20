using UnityEngine;
using System.Collections;

public class MenuItemSelection : MonoBehaviour {

	public delegate void MenuItemSelect(GameObject selectedItem);
	public static event MenuItemSelect OnMenuItemSelected;
	public static event MenuItemSelect OnMenuItemHold;

	private const float holdEventInterval = 0.5f; // given in seconds

	private float elapsedTime = 0.0f;
	private bool isItemBeingHeld;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (isItemBeingHeld) {
			elapsedTime += Time.deltaTime;
		} else {
			elapsedTime = 0.0f;
		}
		if (elapsedTime >= holdEventInterval) {
			if (isItemBeingHeld && OnMenuItemHold != null) {
				OnMenuItemHold(gameObject);
			}
			elapsedTime = 0.0f;
		}
	}

	void OnCollisionEnter(Collision collision) {
		isItemBeingHeld = true;
		if (OnMenuItemSelected != null) {
			OnMenuItemSelected(gameObject);
		}
		if (OnMenuItemHold != null) {
			OnMenuItemHold(gameObject);
		}
	}

	void OnCollisionExit(Collision collision) {
		isItemBeingHeld = false;
	}
}
