using UnityEngine;
using System.Collections;

public class VirtualKeyboard : InteractionPanel
{	
	private KeyActivator activeKey = null;
	private string currentString = "";
	private Mesh mesh;
	private Bounds bounds;
	public GameObject studyObject;
	private Study study;

	// Use this for initialization
	void Start ()
	{
		study = studyObject.GetComponent<Study>();
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
		LeapGestures.HandClosedGestureTriggered += onKeyLeapPressed;
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

	void onKeyLeapPressed(object sender, System.EventArgs e) {
		// Get the active key's value (if there is one) and update the text prompt with the character.
		if (activeKey != null) {
			char inputChar = (activeKey.keyId == "space")
					? ' '
					: activeKey.keyId.ToCharArray()[0];
			Debug.Log("Leap press: " + inputChar);
			study.updateStudyText(inputChar);
		}
	}

	public override void calculateBounds() {
		if (renderer == null) {
			gameObject.AddComponent<MeshRenderer>();
		}
		bounds = renderer.bounds;

		foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
			bounds.Encapsulate(r.bounds);
		}		
	}

	public override bool withinBounds(Vector3 coordinate) {
		return bounds.Contains(coordinate);
	}
}

