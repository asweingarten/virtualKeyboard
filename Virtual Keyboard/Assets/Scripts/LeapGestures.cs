using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Leap;

public class LeapGestures : MonoBehaviour {

	private Controller leapController;
	private int gestureId;

	// Set a timeout for gestures of 1 second - anything longer than this is ignored.
	public float gestureTimeout = 1.0f; 

	// Set a grab threshold for the custom HandClosed gesture.
	public float grabThreshold = 0.95f;
	public float halfGrabThreshold = 0.75f;

	// Once a grab gesture is detected, set a timeout (in ms) so that it can't be performed for a short time afterwards. 
	// This is a form of debouncing the event
	private long grabTimeout = 800;
	private Stopwatch grabTimer = new Stopwatch();

	public delegate void CircularGestureAction(object sender, System.EventArgs e);
	public delegate void KeyTapGestureAction(object sender, System.EventArgs e);
	public delegate void HandClosedGestureAction(object sender, System.EventArgs e);
	public delegate void HandHalfClosedGestureAction(object sender, System.EventArgs e);
	public delegate void HandOpenedGestureAction(object sender, System.EventArgs e);
	public delegate void SwipeGestureAction(SwipeGesture swipe);
	public static event CircularGestureAction CircularGestureTriggered;
	public static event KeyTapGestureAction KeyTapGestureTriggered;
	public static event HandClosedGestureAction HandClosedGestureTriggered;
	public static event HandHalfClosedGestureAction HandHalfClosedGestureTriggered;
	public static event HandOpenedGestureAction HandOpenedGestureTriggered;
	public static event SwipeGestureAction SwipeGestureTriggered;


	private enum HandStates {Open, HalfClosed, Closed};
	private HandStates handState = HandStates.Open;

	void Awake () {
		leapController = new Controller ();
	}

	public float keyTapMinVelocity = 30.0f;
	public float keyTapHistorySeconds = 0.3f;
	public float keyTapMinDistance = 0.7f;

	// Use this for initialization
	void Start () {
		leapController.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
		leapController.EnableGesture (Leap.Gesture.GestureType.TYPECIRCLE);
		leapController.EnableGesture (Leap.Gesture.GestureType.TYPE_KEY_TAP);
		leapController.Config.SetFloat("Gesture.KeyTap.MinDownVelocity", keyTapMinVelocity);
		leapController.Config.SetFloat("Gesture.KeyTap.HistorySeconds", keyTapHistorySeconds);
		leapController.Config.SetFloat("Gesture.KeyTap.MinDistance", keyTapMinDistance);

		leapController.Config.Save();
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
			} else if ( gesture.Type == Gesture.GestureType.TYPE_SWIPE && gesture.State.Equals(Gesture.GestureState.STATESTOP)){
				SwipeGesture swipe = new SwipeGesture(gesture);
				OnSwipeGesture(swipe);
			}
		}
		//gestures.Dispose();//gestureList realted to memory leak?
		if (grabTimer.ElapsedMilliseconds > grabTimeout) {
			HandList hands = frame.Hands;
			foreach (Hand hand in hands) {
				//UnityEngine.Debug.Log ("Grab Strength: " + hand.GrabStrength);
				// Look at the frame's hands. If above the grab threshold, fire the HandClosed event
				if (hand.GrabStrength >= grabThreshold && handState != HandStates.Closed) {
					grabTimer.Reset();
					grabTimer.Start();
					handState = HandStates.Closed;
					OnHandClosedGesture(this, null);
				} else if(hand.GrabStrength < grabThreshold && hand.GrabStrength > halfGrabThreshold && handState != HandStates.HalfClosed){
					grabTimer.Reset();
					grabTimer.Start();
					handState = HandStates.HalfClosed;
					OnHandHalfClosedGesture(this, null);
				} else if(hand.GrabStrength <= halfGrabThreshold && handState != HandStates.Open) {
					grabTimer.Reset();
					grabTimer.Start();
					handState = HandStates.Open;
					OnHandOpenedGesture(this, null);
				}
			}
		}

	}

	private void OnCircularGestureCompleted(object sender, System.EventArgs e) {
		if (CircularGestureTriggered != null) {
			CircularGestureTriggered(sender, e);
		}
	}

	private void OnKeyTapGesture(object sender, System.EventArgs e) {
		if (KeyTapGestureTriggered != null) {
			KeyTapGestureTriggered(sender, e);
		}
	}

	private void OnHandClosedGesture(object sender, System.EventArgs e) {
		if (HandClosedGestureTriggered != null) {
			HandClosedGestureTriggered(sender, e);
		}
	}

	private void OnHandHalfClosedGesture(object sender, System.EventArgs e) {
		if (HandHalfClosedGestureTriggered != null) {
			HandHalfClosedGestureTriggered(sender, e);
		}
	}

	private void OnHandOpenedGesture(object sender, System.EventArgs e) {
		if (HandOpenedGestureTriggered != null) {
			HandOpenedGestureTriggered(sender, e);
		}
	}

	private void OnSwipeGesture(SwipeGesture swipe) {
		if (SwipeGestureTriggered != null) {
			SwipeGestureTriggered(swipe);
		}
	}

}
