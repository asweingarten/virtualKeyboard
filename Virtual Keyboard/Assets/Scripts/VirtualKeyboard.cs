using UnityEngine;
using System.Collections;

public class VirtualKeyboard : InteractionPanel
{	
	private KeyActivator activeKey = null;
	private KeyActivator prevActiveKey = null;

	//private string currentString = "";
	private Mesh mesh;
	private Bounds bounds;
	public GameObject studyObject;
	private Study study;

	public float hoverActivationTime = 0.15f;
	public float typingDelayMultiplier = 2.5f;
	public float debounceTime = 0.3f;
	private KeyActivator debouncedKey;

	private enum TypingState {Enabled, Disabled, Delayed};
	private TypingState typingState = TypingState.Enabled;
	// Use this for initialization
	void Start ()
	{
		study = studyObject.GetComponent<Study>();
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
		LeapGestures.HandClosedGestureTriggered += disableTyping;
		LeapGestures.HandHalfClosedGestureTriggered += enableTyping;
		LeapGestures.HandOpenedGestureTriggered += enableTyping;
		LeapGestures.KeyTapGestureTriggered += onKeyTap;
	}

	
	// Update is called once per frame
	void Update ()
	{
		//MessageLogger.Instance.updateText(currentString);
	}


	void disableTyping(object sender, System.EventArgs e) {
		typingState = TypingState.Disabled;
	}

	void enableTyping(object sender, System.EventArgs e) {
		typingState = TypingState.Enabled;
	}

	void delayTyping(object sender, System.EventArgs e) {
		typingState = TypingState.Delayed;
	}

	void onKeyTap(object sender, System.EventArgs e) {
		if(activeKey != null && (activeKey != debouncedKey)) {
			Debug.Log ("Key Tap");
			StartCoroutine(TypingDebounce(activeKey));
			typeStudyText(activeKey.keyId);
		} else {
			if( activeKey == null ) {
				Debug.Log ("Spurious Key Tap");
			}
		}

	}

	void onKeyLeapFocus(KeyActivator key) {
		if(activeKey != null ) activeKey.setActive (false);
		key.setActive(true);
		activeKey = key;
		//StartCoroutine(WaitAndTriggerKey(key));

	}

	void onKeyLeapFocusLost(KeyActivator key) {
		key.setActive(false);
		if( key != activeKey ) return;
		activeKey = null;
	}



	IEnumerator TypingDebounce(KeyActivator key ) {
		debouncedKey = key;
		yield return new WaitForSeconds(debounceTime);
		debouncedKey = null;

	}

	IEnumerator WaitAndTriggerKey(KeyActivator key) {
		//Extra delay for TypingState.Delayed and repeated characters
		if( typingState == TypingState.Delayed || key == prevActiveKey) yield return new WaitForSeconds(hoverActivationTime*typingDelayMultiplier);
		else yield return new WaitForSeconds(hoverActivationTime);

		prevActiveKey = null;//Clear active key as we have waited long enough now

		//Wait until typing is not disabled or activeKey changes
		while( key == activeKey && typingState == TypingState.Disabled ) {
			yield return new WaitForSeconds(0.5f*hoverActivationTime);
		}

		if( key == activeKey && typingState != TypingState.Disabled) {
			prevActiveKey = activeKey;
			typeStudyText(activeKey.keyId);
		}
	}

	private void typeStudyText(string key) {
		char inputChar = (key == "space")
			? ' '
			: key.ToCharArray()[0];
		//Debug.Log("Leap press: " + inputChar);
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

