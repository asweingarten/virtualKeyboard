using UnityEngine;
using System.Collections;

public class ManipulationSwitcher : MonoBehaviour {

	public GameObject itemManipulatorObject;

	public bool translationEnabler = false;
	public bool rotationEnabler = false;
	public bool scalingEnabler = false;

	private ItemManipulator itemManipulator;

	void Awake () {
		if( itemManipulatorObject == null ) return;
		itemManipulator = itemManipulatorObject.GetComponent<ItemManipulator> ();
		if (itemManipulator == null) return;
		ArcMeshDrawer.OnRadialMenuActionTrigger += changeManipulatorMode;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void changeManipulatorMode(GameObject source) {
		Debug.Log ("changeManipulatorMode");
		if (source != gameObject) return;
		itemManipulator.translationEnabled = translationEnabler;
		itemManipulator.rotationEnabled = rotationEnabler;
		itemManipulator.scalingEnabled = scalingEnabler;

	}
}
