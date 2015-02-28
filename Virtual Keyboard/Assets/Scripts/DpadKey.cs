using UnityEngine;
using System.Collections;

public class DpadKey : MonoBehaviour {
	public KeyCode id;

	// Use this for initialization
	void Start () {
		DpadController.OnLeapRotateOn += activate;
		DpadController.OnLeapRotateOff += deactivate;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void activate(KeyCode keyActivated) {
		if (id != keyActivated) return;
		Debug.Log(id.ToString() + " activated!");
	}

	void deactivate(KeyCode keyDeactivated) {
		if (id != keyDeactivated) return;
		Debug.Log(id.ToString() + " deactivated!");
	}
}
