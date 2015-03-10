using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractionPanel))]
public class VirtualKeyboard : MonoBehaviour
{
	// Define the user that the keyboard is associated with - set via the editor
	public GameObject user;
	
	private InteractionPanel interactionPanel;
	private KeyActivator activeKey = null;
	public BoxCollider collider;

	// Use this for initialization
	void Start ()
	{
		//collider = gameObject.AddComponent<BoxCollider>();
		//Bounds bounds = getKeyboardBounds();

		//collider.center = bounds.center;
		//collider.size = bounds.size;

		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;

		interactionPanel = GetComponent<InteractionPanel>();
		if (interactionPanel != null) {
			interactionPanel.OnAction += onKeyLeapPressed;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void onKeyLeapFocus(KeyActivator key) {
		key.setActive(true);
		activeKey = key;
	}

	void onKeyLeapFocusLost(KeyActivator key) {
		key.setActive(false);
		activeKey = null;
	}

	void onKeyLeapPressed() {
		// Get the active key's value (if there is one) and update the text prompt with the character.
		if (activeKey != null) {
			Debug.Log("Active key pressed: " + activeKey.keyId);
		}
	}

/*
	Bounds getKeyboardBounds() {

		Vector3 min = new Vector3(10000,10000,10000);
		Vector3 max = new Vector3(-10000,-10000,-10000);

		Collider[] childColliders = gameObject.GetComponentsInChildren<Collider>();
		foreach(Collider childCollider in childColliders) {
			if (childCollider != (Collider) collider) {
				Vector3 minBounds = childCollider.bounds.min;
				Vector3 maxBounds = childCollider.bounds.max;

				min.x = Mathf.Min(min.x, minBounds.x);
				min.y = Mathf.Min(min.y, minBounds.y);
				min.z = Mathf.Min(min.z, minBounds.z);

				max.x = Mathf.Max(max.x, maxBounds.x);
				max.y = Mathf.Max(max.y, maxBounds.y);
				max.z = Mathf.Max(max.z, maxBounds.z);

				Debug.Log("Comparing bounds. Min: " + minBounds + " , Max: " + maxBounds);
			}
        }
             

		Debug.Log("Adding new bounds. Min: " + min + " , Max: " + max);
		Vector3 size = max - min;
		Debug.Log("Size: " + size);
		Bounds bounds = new Bounds();
		bounds.SetMinMax(min,max);
        return bounds;
	} */

}

