using UnityEngine;
using System.Collections;

public class RadialActionSelection : MonoBehaviour {

	public delegate void RadialActionOnSelect(GameObject selectedItem);
	public static event RadialActionOnSelect OnRadialActionSelected;

	public string selectionObjectName { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		GameObject collidingObject = collision.gameObject;
		if( selectionObjectName != null && selectionObjectName.Length != 0 && collidingObject.name != selectionObjectName ) {
			Transform parentTransform = collidingObject.transform.parent;
			if( parentTransform == null || parentTransform.gameObject.name != selectionObjectName ) {
				return;
			}
		}

		if (OnRadialActionSelected != null) {
			OnRadialActionSelected(gameObject);
		}
	}
}
