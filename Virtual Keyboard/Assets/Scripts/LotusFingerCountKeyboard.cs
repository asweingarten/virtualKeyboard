using UnityEngine;
using System.Collections;

public class LotusFingerCountKeyboard : MonoBehaviour {
	 
	public GameObject studyObject;
	private Study study;
	
	void Awake() {
		LotusClusterFingerCountBoundary.LotusClusterSelected += onLotusClusterSelected;
	}
	
	// Use this for initialization
	void Start () {
		study = studyObject.GetComponent<Study>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onLotusClusterSelected(GameObject cluster) {
		LotusFingerCountCluster fingerCountCluster = cluster.GetComponent<LotusFingerCountCluster>();
		Debug.Log ("fingerCountCluster: " +fingerCountCluster == null);
		if( fingerCountCluster != null ) {
			Debug.Log("in if");
			KeyActivator activeKey = fingerCountCluster.getActiveKey();
			Debug.Log("activeKey");
			if( activeKey != null ) {
				typeStudyText(activeKey.keyId);
			}
		}
	}

	private void typeStudyText(string key) {
		key = key.ToLower();
		char inputChar = (key == "space")
			? ' '
				: key.ToCharArray()[0];
		study.updateStudyText(inputChar);
	}
}
