using UnityEngine;
using System.Collections;

public class RadialActionSelection : MonoBehaviour {

	public delegate void RadialActionOnSelect(GameObject selectedItem);
	public static event RadialActionOnSelect OnRadialActionSelected;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (OnRadialActionSelected != null) {
			OnRadialActionSelected(gameObject);
		}
	}
}
