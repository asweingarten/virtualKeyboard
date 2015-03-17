using UnityEngine;
using System.Collections;
using Leap;

public class MenuGlobalShortcuts : MonoBehaviour {

	public string clockwiseMenuKey = "m";
	public string counterClockwiseMenuKey = "n";
	public bool isAnythingSelected = false;
	
	private Controller leapController;
	ItemManipulator itemManipulator;
	GameObject furnitureList;
	GameObject nestedRadialMenu;
	
	void Awake () {
		leapController = new Controller ();
	}

	// Use this for initialization
	void Start () {
		itemManipulator = GameObject.Find("ItemManipulator").transform.GetComponent<ItemManipulator>();
		furnitureList = GameObject.Find("FurnitureList");
		furnitureList.SetActive(false);
		nestedRadialMenu = GameObject.Find("NestedRadialMenu");
		nestedRadialMenu.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {

		isAnythingSelected = itemManipulator.isAnythingSelected ();

		if (!isAnythingSelected) {
			Frame frame = leapController.Frame ();
			GestureList gestures = frame.Gestures();
			foreach (Gesture gesture in gestures) {
				if( gesture.State.Equals(Gesture.GestureState.STATESTOP) ) {
					if( gesture.Type == Leap.Gesture.GestureType.TYPECIRCLE ) {
						CircleGesture circleGesture = new CircleGesture(gesture);

						if( circleGesture.Pointable.Direction.AngleTo(circleGesture.Normal) <= Mathf.PI/2 ) {
							furnitureList.SetActive(true);
						}
						else {
							furnitureList.SetActive(false);
						}

					}
					return;
				}
			}
		}
	}
}
