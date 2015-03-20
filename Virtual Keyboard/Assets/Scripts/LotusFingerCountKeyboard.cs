using UnityEngine;
using System.Collections;

public class LotusFingerCountKeyboard : MonoBehaviour {
	 
	public GameObject studyObject;
	public AudioSource clickSound = null;
	public TextReceiver textReceiver = null;

	void Awake() {
		LotusClusterFingerCountBoundary.LotusClusterSelected += onLotusClusterSelected;
	}

	void onLotusClusterSelected(GameObject cluster) {
		LotusFingerCountCluster fingerCountCluster = cluster.GetComponent<LotusFingerCountCluster>();
		if( fingerCountCluster != null ) {
			KeyActivator activeKey = fingerCountCluster.getActiveKey();
			if( activeKey != null ) {
				typeText(activeKey.keyId);
			}
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
