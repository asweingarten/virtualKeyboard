using UnityEngine;
using System.Collections;

public class CursorGestureSwitch : MonoBehaviour {

	public Material enabledMaterial;
	public Material disabledMaterial;

	// Use this for initialization
	void Start () {
		renderer.sharedMaterial = enabledMaterial;
		LeapGestures.HandClosedGestureTriggered += setDisabledMaterial;
		LeapGestures.HandHalfClosedGestureTriggered += setEnabledMaterial;
		LeapGestures.HandOpenedGestureTriggered += setEnabledMaterial;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnDestroy() {
		LeapGestures.HandClosedGestureTriggered -= setDisabledMaterial;
		LeapGestures.HandHalfClosedGestureTriggered -= setEnabledMaterial;
		LeapGestures.HandOpenedGestureTriggered -= setEnabledMaterial;
	}

	void setEnabledMaterial(object sender, System.EventArgs e) {
		if( renderer != null ) renderer.sharedMaterial = enabledMaterial;
	}

	void setDisabledMaterial(object sender, System.EventArgs e) {
		if( renderer != null ) renderer.sharedMaterial = disabledMaterial;
	}
}
