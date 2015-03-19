using UnityEngine;
using System.Collections;

public class FingerCountColorSwitcher : MonoBehaviour {
	
	private KeyActivator[] keyActivators;
	private KeyActivator activeKeyActivator;

	void Awake () {
		keyActivators = GetComponentsInChildren<KeyActivator>();
		LeapCursor.OnFingerCountChanged += updateActiveKey;
	}

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateActiveKey(int numFingers) {
		int keyTarget = numFingers - 1;
		if( keyTarget >= keyActivators.Length ) return;
		if( activeKeyActivator != null ) activeKeyActivator.setActive(false);
		if( keyTarget < 0 ) return;
		keyActivators[keyTarget].setActive(true);
		activeKeyActivator = keyActivators[keyTarget];
	}
}
