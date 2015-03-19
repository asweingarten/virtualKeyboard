using UnityEngine;
using System.Collections;

public class LotusCluster : MonoBehaviour {

	public string[] characters = new string[4];
	private Vector3 originalLocation;

	public enum AllignStrategy { HORIZONTAL, VERTICAL };
	public AllignStrategy allignStrategy;
	private GameObject boundary;

	void Awake() {
		boundary = transform.FindChild("Boundary").gameObject;
		boundary.transform.position = transform.position;
		originalLocation = transform.localPosition;
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
		boundary.transform.localScale = new Vector3(1.75f, 0.01f, 1.75f);
		setKeyActivation(true);
	}

	public void deactivate() {
		setKeyActivation(false);
		transform.localPosition = originalLocation;
		boundary.transform.localScale = new Vector3(1.25f,0.01f,1.25f);

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
				key.transform.localPosition = new Vector3( (-3*keySize.x) + i*2*keySize.x, 0, 0);
				break;
			case AllignStrategy.HORIZONTAL:
				key.transform.localPosition = new Vector3( 0, (-3*keySize.y) + i*2*keySize.y, 0);
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
