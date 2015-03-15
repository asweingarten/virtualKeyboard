using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Leap;

public class LeapGestures : MonoBehaviour {

	private Controller leapController;
	private int gestureId;

	GameObject[] interactionPanels;
	private int interactionPanel = 0;

	// Set a timeout for gestures of 1 second - anything longer than this is ignored.
	public float gestureTimeout = 1.0f; 

	// Set a grab threshold for the custom HandClosed gesture.
	public float grabThreshold = 0.95f;

	// Once a grab gesture is detected, set a timeout (in ms) so that it can't be performed for a short time afterwards. 
	// This is a form of debouncing the event
	private long grabTimeout = 800;
	private Stopwatch grabTimer = new Stopwatch();

	public delegate void CircularGestureAction(object sender, System.EventArgs e);
	public delegate void KeyTapGestureAction(object sender, System.EventArgs e);
	public delegate void HandClosedGestureAction(object sender, System.EventArgs e);
	public static event CircularGestureAction CircularGestureTriggered;
	public static event KeyTapGestureAction KeyTapGestureTriggered;
	public static event HandClosedGestureAction HandClosedGestureTriggered;

	void Awake () {
		leapController = new Controller ();
	}
	// Use this for initialization
	void Start () {

		leapController.EnableGesture (Leap.Gesture.GestureType.TYPECIRCLE);
		leapController.EnableGesture (Leap.Gesture.GestureType.TYPE_KEY_TAP);
		grabTimer.Start();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = leapController.Frame();
		GestureList gestures = frame.Gestures();

		foreach (Gesture gesture in gestures) {
			// Ignore any gestures that have been going on for more than a second.
			if (gesture.DurationSeconds > gestureTimeout) {
				continue;
			}

			// Otherwise, check if it is a supported gesture and peform the appropriate event.
			if (gesture.Type == Gesture.GestureType.TYPE_KEY_TAP) {
				OnKeyTapGesture(this, null);
			} 
		 	else if (gesture.Type == Gesture.GestureType.TYPECIRCLE && gesture.State.Equals(Gesture.GestureState.STATESTOP)) {
				OnCircularGestureCompleted(this, null);
			}
		}

		if (grabTimer.ElapsedMilliseconds > grabTimeout) {
			HandList hands = frame.Hands;
			foreach (Hand hand in hands) {
				// Look at the frame's hands. If above the grab threshold, fire the HandClosed event
				if (hand.GrabStrength >= grabThreshold) {
					grabTimer.Reset();
					grabTimer.Start();
					OnHandClosedGesture(this, null);
					return;
				}
			}
		}
	}

	private void OnCircularGestureCompleted(object sender, System.EventArgs e) {
		// switchInputType();
		if (CircularGestureTriggered != null) {
			CircularGestureTriggered(sender, e);
			UnityEngine.Debug.Log("Circular gesture");
		}
	}

	private void OnKeyTapGesture(object sender, System.EventArgs e) {
		if (KeyTapGestureTriggered != null) {
			KeyTapGestureTriggered(sender, e);
			UnityEngine.Debug.Log("Key tap gesture");
		}
	}

	private void OnHandClosedGesture(object sender, System.EventArgs e) {
		UnityEngine.Debug.Log("Hand closed gesture");
		if (HandClosedGestureTriggered != null) {
			HandClosedGestureTriggered(sender, e);
		}
	}

	void switchInputType() {
		interactionPanels[interactionPanel%2].SetActive(false);

		interactionPanel++;

		interactionPanels[interactionPanel%2].SetActive(true);
	}
}
