using UnityEngine;
using System.Collections;

public class LotusKeyboard : MonoBehaviour {
	
	GameObject activeCluster;
	KeyActivator activeKey;
	public bool deactiveClusterAfterKeyType = false;
	public AudioSource clickSound = null;
	public TextReceiver textReceiver = null;

	void Awake() {
		LotusClusterBoundary.LotusClusterSelected += updateActiveLotusCluster;
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
	}
	
	void updateActiveLotusCluster(GameObject lotusCluster) {
		if (activeCluster == lotusCluster) return;

		if (activeCluster != null) {
			LotusCluster activeLotusCluster = activeCluster.GetComponent<LotusCluster>();
			if( activeLotusCluster != null ) activeLotusCluster.deactivate();
		}

		LotusCluster cluster = lotusCluster.GetComponent<LotusCluster>();
		if (cluster != null) {
			cluster.activate();
			activeCluster = lotusCluster;
			Debug.Log ("Cluster made active");
		}
	}

	void deactiveActiveLotusCluster() {
		if( activeCluster == null ) return;

		LotusCluster activeLotusCluster = activeCluster.GetComponent<LotusCluster>();
		if( activeLotusCluster != null ) activeLotusCluster.deactivate();
		activeCluster = null;
	}

	void onKeyLeapFocus(KeyActivator key) {
		if (key == activeKey || key.isActive) return;

		activeKey = key;
		key.setActive(true);
		typeText(key.keyId);
		if (clickSound != null) { clickSound.Play(); }
		if(deactiveClusterAfterKeyType) deactiveActiveLotusCluster();
	}
	
	void onKeyLeapFocusLost(KeyActivator key) {
		if (!key.isActive) return;
		key.setActive(false);
		if( key != activeKey ) return;
		activeKey = null;
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
