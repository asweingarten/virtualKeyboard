using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Leap;

public class LotusFingerCountKeyboard : MonoBehaviour {
	 
	public AudioSource clickSound = null;
	public TextReceiver textReceiver = null;
	public float swipeTimeout = 1.0f;

	public long swipeStartDebounceMilliseconds = 500;
	private Stopwatch swipeStartTimer = new Stopwatch();
	public long swipeStopDebounceMilliseconds = 500;
	private Stopwatch swipeStopTimer = new Stopwatch();
	private enum SwipeState {None, Started };
	private SwipeState swipeState = SwipeState.None;

	private List<KeyActivator> typedKeys;

	void Awake() {
		LotusClusterFingerCountBoundary.LotusClusterSelected += onLotusClusterSelected;
		LeapGestures.SwipeGestureTriggered += onSwipeGesture;
		typedKeys = new List<KeyActivator>();
	}

	void Start() {
		swipeStartTimer.Start();
		swipeStopTimer.Start();
	}

	void onLotusClusterSelected(GameObject cluster) {
		LotusFingerCountCluster fingerCountCluster = cluster.GetComponent<LotusFingerCountCluster>();
		if( fingerCountCluster != null ) {
			KeyActivator activeKey = fingerCountCluster.getActiveKey();
			if( activeKey != null ) {
				if( swipeState.Equals( SwipeState.None ) ) {
					typeText(activeKey.keyId);
				} else if( swipeState.Equals( SwipeState.Started )) {
					typedKeys.Add(activeKey);
				}
			}
		}
	}

	private Frame lastFrame;
	void onSwipeGesture(SwipeGesture swipeGesture) {
		if( lastFrame != null && swipeGesture.Frame.Equals(lastFrame) ) return;
		switch(swipeGesture.State) {
			case Gesture.GestureState.STATESTART:
				if( swipeStartTimer.ElapsedMilliseconds < swipeStartDebounceMilliseconds ) return;
				swipeStartTimer.Reset();
				swipeStartTimer.Start();
				lastFrame = swipeGesture.Frame;
				UnityEngine.Debug.Log("SwipeGesture: " + swipeGesture.Id);
				StopCoroutine("swipeGestureStarted");
				swipeGestureStarted(swipeGesture);
				StartCoroutine(swipeGestureStarted(swipeGesture));
				break;
			case Gesture.GestureState.STATESTOP:
				if( swipeStopTimer.ElapsedMilliseconds < swipeStopDebounceMilliseconds ) return;
				swipeStopTimer.Reset();
				swipeStopTimer.Start();
				StopCoroutine("swipeGestureStarted");
				swipeState = SwipeState.None;
				UnityEngine.Debug.Log("SwipeGesture: " + swipeGesture.Id + " " + swipeGesture.Direction);

				if( Mathf.Abs(swipeGesture.Direction.x) > Mathf.Abs(swipeGesture.Direction.y) ) {
					if( swipeGesture.Direction.x > 0 ) {
						typeText(KeyCode.Space);
					} else {
						typeText(KeyCode.Backspace);
					}
				}
				//Want left swipe to be back space
				//Want right swipe to be space
				break;
		}
		
	}

	private IEnumerator swipeGestureStarted(SwipeGesture swipeGesture) {
		typedKeys.Clear();
		swipeState = SwipeState.Started;
		yield return new WaitForSeconds(swipeTimeout);
		swipeState = SwipeState.None;
		foreach( KeyActivator typedKey in typedKeys ) {
			typeText( typedKey.keyId );
		}
	}

	private void typeText(KeyCode keyCode) {
		if (clickSound != null) { 
			clickSound.Play(); 
		}
		if (textReceiver != null) {
			textReceiver.receiveText(keyCode);
		}
	}

	private void typeText(string key) {
		key = key.ToLower();
		char inputChar = (key == "space")
			? ' '
				: key.ToCharArray()[0];
		KeyCode id = new KeyCode();
		if (clickSound != null) { clickSound.Play(); }
		if (textReceiver != null) {
			textReceiver.receiveText(inputChar.ToString());
		} else {
			UnityEngine.Debug.LogWarning("No text receiver specified!");
		}
	}
}
