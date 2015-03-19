using UnityEngine;
using System.Collections;

public class SlidingListInteractionOnCollision : MonoBehaviour {

	public delegate void SelectObject( GameObject selected );
	public static event SelectObject OnSelect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter ( Collider collider ) {
		if( OnSelect != null ) {
			OnSelect( gameObject );
		}
	}

}
