using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent (typeof (ItemSelection))]
public class MenuShortcuts : MonoBehaviour {

	public char menuKey = 'm';

	private Controller leapController;
	private ItemSelection itemSelection;
	public GameObject menuObject;

	void Awake () {
		leapController = new Controller ();
	}

	// Use this for initialization
	void Start () {
		itemSelection = GetComponent<ItemSelection> ();
		leapController.EnableGesture (Leap.Gesture.GestureType.TYPECIRCLE);
		//leapController.config ();
		leapController.Config.SetFloat ("Gesture.Circle.MinRadius", 15.0f);
		leapController.Config.SetFloat ("Gesture.Circle.MinArc", 2*Mathf.PI);
		//Leap.Gesture.Circle.MinRadius = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		if( !isSelected() ) return;

		if (Input.GetKeyDown (""+menuKey)) {
			menuObject.SetActive(!menuObject.activeSelf);
			return;
		}

		Frame frame = leapController.Frame ();
		GestureList gestures = frame.Gestures();
		
		foreach (Gesture gesture in gestures) {
			if( gesture.State.Equals(Gesture.GestureState.STATESTOP) ) {
				menuObject.SetActive(!menuObject.activeSelf);
				return;
			}
		}
	}

	private bool isSelected() {
		return itemSelection.selected;
	}
}
