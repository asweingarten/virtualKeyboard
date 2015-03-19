using UnityEngine;
using System.Collections;

public class LotusClusterBoundary : MonoBehaviour {

	public delegate void UpdateActiveLotusCluster(GameObject LotusCluster);
	public static event UpdateActiveLotusCluster LotusClusterSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collision) {
		Debug.Log ("Cluster selected");
		if (LotusClusterSelected != null ) LotusClusterSelected(transform.parent.gameObject);
	}
}
