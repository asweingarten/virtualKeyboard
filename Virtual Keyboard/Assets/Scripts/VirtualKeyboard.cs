using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractionPanel))]
public class VirtualKeyboard : MonoBehaviour
{	
	private InteractionPanel interactionPanel;
	private KeyActivator activeKey = null;
	private string currentString = "";

	// Use this for initialization
	void Start ()
	{
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
}

