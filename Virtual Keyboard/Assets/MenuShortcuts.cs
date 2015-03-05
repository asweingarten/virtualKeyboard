using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent (typeof (ItemSelection))]
public class MenuShortcuts : MonoBehaviour {

	public string clockwiseMenuKey = "m";
	public string counterClockwiseMenuKey = "n";

	private Controller leapController;
	private ItemSelection itemSelection;
	public GameObject clockwiseMenu;
	public GameObject counterClockwiseMenu;


	void Awake () {
		leapController = new Controller ();
	}

	// Use this for initialization
	void Start () {
		itemSelection = GetComponent<ItemSelection> ();
		leapController.EnableGesture (Leap.Gesture.GestureType.TYPECIRCLE);
		leapController.Config.SetFloat ("Gesture.Circle.MinRadius", 15.0f);
		leapController.Config.SetFloat ("Gesture.Circle.MinArc", 2*Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
		if( !isSelected() ) return;

		if (Input.GetKeyDown (clockwiseMenuKey)) {
			toggleClockwiseMenu();
			return;
		} else if (Input.GetKeyDown (counterClockwiseMenuKey)) {
			toggleCounterClockwiseMenu();
			return;
		}

		Frame frame = leapController.Frame ();
		GestureList gestures = frame.Gestures();
		
		foreach (Gesture gesture in gestures) {
			if( gesture.State.Equals(Gesture.GestureState.STATESTOP) ) {
				if( gesture.Type == Leap.Gesture.GestureType.TYPECIRCLE ) {
					CircleGesture circleGesture = new CircleGesture(gesture);
					//Clockwise
					if( circleGesture.Pointable.Direction.AngleTo(circleGesture.Normal) <= Mathf.PI/2 ) {
						toggleClockwiseMenu();
					}//Counter-Clockwise
					else {
						toggleCounterClockwiseMenu();
					}
				}
				return;
			}
		}
	}

	private void toggleClockwiseMenu() {
		if (clockwiseMenu != null) {
			if(clockwiseMenu.activeSelf) {
				itemSelection.enableSelection();
				clockwiseMenu.SetActive (false);
			} else {
				itemSelection.disableSelection();
				clockwiseMenu.SetActive (true);
			}
		}
		else return;
		if(counterClockwiseMenu != null ) counterClockwiseMenu.SetActive(false);

	}

	private void toggleCounterClockwiseMenu() {
		if (counterClockwiseMenu != null) {
			if( counterClockwiseMenu.activeSelf) {
				itemSelection.enableSelection();
				counterClockwiseMenu.SetActive (false);
			} else {
				itemSelection.disableSelection();
				counterClockwiseMenu.SetActive(true);
			}
		}
		else return;
		if(clockwiseMenu != null ) clockwiseMenu.SetActive(false);

	}

	private bool isSelected() {
		return itemSelection.selected;
	}
}
