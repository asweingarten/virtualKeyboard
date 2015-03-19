using UnityEngine;
using System.Collections;

public class LotusFingerCountCluster : MonoBehaviour {
	
	private KeyActivator[] keyActivators;

	void Awake() {
		keyActivators = GetComponentsInChildren<KeyActivator>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public KeyActivator getActiveKey() {
		foreach( KeyActivator keyActivator in keyActivators ) {
			if( keyActivator.isActive ) {
				return keyActivator;
			}
		}
		return null;
	}
}


