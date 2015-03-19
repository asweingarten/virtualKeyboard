using UnityEngine;
using System.Collections;

public class LotusClusterFingerCountBoundary : MonoBehaviour {

	public delegate void LotusClusterActivated(GameObject LotusCluster);
	public static event LotusClusterActivated LotusClusterSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		if (LotusClusterSelected != null ) LotusClusterSelected(transform.parent.gameObject);
	}
}
