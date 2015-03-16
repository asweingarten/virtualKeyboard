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

	public float hoverActivationTime = 0.15f;
	private bool isHandOpen = true;
	// Use this for initialization
	void Start ()
	{
		study = studyObject.GetComponent<Study>();
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
		LeapGestures.HandClosedGestureTriggered += onHandClosed;
		LeapGestures.HandOpenedGestureTriggered += onHandOpened;
	}
	
	// Update is called once per frame
	void Update ()
	{
		MessageLogger.Instance.updateText(currentString);
	}


	void onHandOpened(object sender, System.EventArgs e) {
		isHandOpen = true;
	}

	void onHandClosed(object sender, System.EventArgs e) {
		isHandOpen = false;
	}


	void onKeyLeapFocus(KeyActivator key) {
		if(activeKey != null ) activeKey.setActive (false);
		key.setActive(true);
		activeKey = key;
		StartCoroutine(WaitAndTriggerKey(key));

	}

	void onKeyLeapFocusLost(KeyActivator key) {
		key.setActive(false);
		if( key != activeKey ) return;
		activeKey = null;
	}
	
	IEnumerator WaitAndTriggerKey(KeyActivator key) {
		yield return new WaitForSeconds(hoverActivationTime);
		if( key == activeKey && isHandOpen) {
			typeStudyText(activeKey.keyId);
		}

	}

	private void typeStudyText(string key) {
		char inputChar = (key == "space")
			? ' '
			: key.ToCharArray()[0];
		Debug.Log("Leap press: " + inputChar);
		study.updateStudyText(inputChar);
	}

	void onKeyLeapPressed(object sender, System.EventArgs e) {
		// Get the active key's value (if there is one) and update the text prompt with the character.
		/*if (activeKey != null) {
			char inputChar = (activeKey.keyId == "space")
					? ' '
					: activeKey.keyId.ToCharArray()[0];
			Debug.Log("Leap press: " + inputChar);
			study.updateStudyText(inputChar);
		}*/
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

