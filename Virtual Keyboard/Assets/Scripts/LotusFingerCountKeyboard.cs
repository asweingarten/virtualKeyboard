using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class LotusFingerCountKeyboard : MonoBehaviour {
	 
	public AudioSource clickSound = null;
	public TextReceiver textReceiver = null;
	public float swipeTimeout = 1.0f;
	private enum SwipeState {None, Started };
	private SwipeState swipeState = SwipeState.None;

	private List<KeyActivator> typedKeys;

	void Awake() {
		LotusClusterFingerCountBoundary.LotusClusterSelected += onLotusClusterSelected;
		LeapGestures.SwipeGestureTriggered += onSwipeGesture;
		typedKeys = new List<KeyActivator>();
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
				lastFrame = swipeGesture.Frame;
				Debug.Log("SwipeGesture: " + swipeGesture.Id);
				StopCoroutine("swipeGestureStarted");
				swipeGestureStarted(swipeGesture);
				StartCoroutine(swipeGestureStarted(swipeGesture));
				break;
			case Gesture.GestureState.STATESTOP:
				StopCoroutine("swipeGestureStarted");
				swipeState = SwipeState.None;
				Debug.Log("SwipeGesture: " + swipeGesture.Id + " " + swipeGesture.Direction);

				if( Mathf.Abs(swipeGesture.Direction.x) > Mathf.Abs(swipeGesture.Direction.y) ) {
					if( swipeGesture.Direction.x > 0 ) {
						typeText("-");
					} else {
						typeText("_");
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

	private void typeText(string key) {
		key = key.ToLower();
		char inputChar = (key == "space")
			? ' '
				: key.ToCharArray()[0];
		if (clickSound != null) { clickSound.Play(); }
		if (textReceiver != null) {
			textReceiver.receiveText(inputChar.ToString());
		} else {
			Debug.LogWarning("No text receiver specified!");
		}
	}
}
