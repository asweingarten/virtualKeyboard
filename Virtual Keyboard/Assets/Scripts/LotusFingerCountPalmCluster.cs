using UnityEngine;
using System.Collections;

public class LotusFingerCountPalmCluster : MonoBehaviour {
	
	private KeyActivator[] keyActivators;
	private KeyActivator activeIndex;

	void Awake() {
		keyActivators = GetComponentsInChildren<KeyActivator>();
	}

	//Keys will be 0 indexed
	public void setActiveKeyIndex( int index ) {
		if( activeIndex != null ) activeIndex.setActive(false);

		//If index is out of bounds return;
		if( index < 0 || index >= keyActivators.Length ) return;
		activeIndex = keyActivators[index];
		activeIndex.setActive(true);
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


