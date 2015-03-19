using UnityEngine;
using System.Collections;

public class LotusKeyboard : MonoBehaviour {
	
	GameObject activeCluster;
	public GameObject studyObject;
	private Study study;
	KeyActivator activeKey;
	public bool deactiveClusterAfterKeyType = false;
	
	void Awake() {
		LotusClusterBoundary.LotusClusterSelected += updateActiveLotusCluster;
		KeyActivator.OnKeyLeapFocus += onKeyLeapFocus;
		KeyActivator.OnKeyLeapFocusLost += onKeyLeapFocusLost;
	}
	
	// Use this for initialization
	void Start () {
		study = studyObject.GetComponent<Study>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		typeStudyText(key.keyId);
		if(deactiveClusterAfterKeyType) deactiveActiveLotusCluster();
	}
	
	void onKeyLeapFocusLost(KeyActivator key) {
		if (!key.isActive) return;
		key.setActive(false);
		if( key != activeKey ) return;
		activeKey = null;
	}

	private void typeStudyText(string key) {
		key = key.ToLower();
		char inputChar = (key == "space")
			? ' '
				: key.ToCharArray()[0];
		study.updateStudyText(inputChar);
	}
}
