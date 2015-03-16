using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractionPanel))]
public class VirtualKeyboard : InteractionPanel
{	
	private KeyActivator activeKey = null;
	private string currentString = "";

	// Use this for initialization
	void Start ()
	{
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
	}
	
	// Update is called once per frame
	void Update ()
	{
		MessageLogger.Instance.updateText(currentString);
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
			if (activeKey.keyId == "space") {
				currentString += " ";
			} else {
				currentString += activeKey.keyId;
			}
		}
	}


	public override Vector3 calculateSize() {
		Vector3 minBounds = new Vector3(1000, 1000, 1000);
		Vector3 maxBounds = new Vector3(-1000, -1000, -1000);
		
		Collider[] childColliders = GetComponentsInChildren<Collider>();
		foreach(Collider childCollider in childColliders) {
			if (childCollider != null) {
				Vector3 min = childCollider.bounds.min;
				Vector3 max = childCollider.bounds.max;
				
				minBounds.x = Mathf.Min(min.x, minBounds.x);
				minBounds.y = Mathf.Min(min.y, minBounds.y);
				minBounds.z = Mathf.Min(min.z, minBounds.z);
				
				maxBounds.x = Mathf.Max(max.x, maxBounds.x);
				maxBounds.y = Mathf.Max(max.y, maxBounds.y);
				maxBounds.z = Mathf.Max(max.z, maxBounds.z);
			}
		}
		return maxBounds - minBounds;
		//interactionPanelSize = (maxBounds - minBounds);
		//Debug.Log("Panel Size: " + interactionPanelSize);
	}
}

