using UnityEngine;
using System.Collections;

public class LotusCluster : MonoBehaviour {

	public string[] characters = new string[4];
	private Vector3 originalLocation;

	public enum AllignStrategy { HORIZONTAL, VERTICAL };
	public AllignStrategy allignStrategy;
	private GameObject boundary;

	private Vector3 originalScale;
	public float selectedScaleFactor = 1.5f;

	private Vector3 originalBoundaryScale;
	public float selectedBoundaryScaleFactor = 1.5f;

	public float activeKeySpaceFactor = 1.25f;

	void Awake() {
		boundary = transform.FindChild("Boundary").gameObject;
		boundary.transform.position = transform.position;
		originalBoundaryScale = boundary.transform.localScale;
		originalLocation = transform.localPosition;
		originalScale = transform.localScale;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void activate() {
		transform.localPosition = Vector3.zero;
		allignKeys();
		transform.localScale = new Vector3(originalScale.x * selectedScaleFactor, 
		                                   originalScale.y * selectedScaleFactor, 
		                                   originalScale.z );
		boundary.transform.localScale = new Vector3(originalBoundaryScale.x * selectedBoundaryScaleFactor, 
		                                   			originalBoundaryScale.y, 
		                                  			originalBoundaryScale.z * selectedBoundaryScaleFactor);
		setKeyActivation(true);
	}

	public void deactivate() {
		setKeyActivation(false);
		transform.localPosition = originalLocation;
		transform.localScale = originalScale;
		boundary.transform.localScale = originalBoundaryScale;

		GameObject keys = transform.FindChild("keys").gameObject;
		CircleAranger arranger = keys.GetComponent<CircleAranger>();

		if (arranger != null) arranger.arrageCircle();
	}

	void allignKeys() {
		Transform keys = transform.FindChild("keys");
		for (int i = 0; i < keys.childCount; i++) {
			Transform key = keys.GetChild(keys.childCount-1-i);
			Vector3 keySize = key.transform.localScale;

			switch (allignStrategy) {
			case AllignStrategy.VERTICAL:
				key.transform.localPosition = new Vector3( (-3*activeKeySpaceFactor*keySize.x) + i*2*activeKeySpaceFactor*keySize.x, 0, 0);
				break;
			case AllignStrategy.HORIZONTAL:
				key.transform.localPosition = new Vector3( 0, (-3*activeKeySpaceFactor*keySize.y) + i*2*activeKeySpaceFactor*keySize.y, 0);
				break;
			}
		}
	}

	void setKeyActivation(bool isActive) {
		Transform keys = transform.FindChild("keys");
		for (int i = 0; i < keys.childCount; i++) {
			GameObject key = keys.GetChild(i).gameObject;
			KeyActivator keyActivator = key.GetComponent<KeyActivator>();

			if (keyActivator != null) {
				keyActivator.enabled = isActive;
			}
		}
	}
}
