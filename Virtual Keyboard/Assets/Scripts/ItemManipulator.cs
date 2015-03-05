using UnityEngine;
using System.Collections;
using Leap;

public class ItemManipulator : MonoBehaviour
{
	private GameObject selectedItem;
	 
	private Controller controller;

	private bool trackingActive = false;

	public bool translationEnabled = false;
	public bool rotationEnabled = false;
	public bool scalingEnabled = false;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		ItemSelection.OnItemSelected += updateSelectedItem;
	}

	// Update is called once per frame
	void Update () {
		if (selectedItem == null) return;

		//translationEnabled = isHandClosed ();

		if (translationEnabled) {
			translateItem ();
		}
		if (rotationEnabled) {
			rotateItem();
		}
		if (scalingEnabled) {
			scaleItem();
		}
	}

	bool isHandClosed(){
		Frame frame = controller.Frame ();

		Hand hand = frame.Hands.Rightmost;
		return (hand.GrabStrength > 0.9f);
	}

	void updateSelectedItem(GameObject newlySelectedItem) {
		if (selectedItem != null && selectedItem.particleSystem != null) {
			selectedItem.particleSystem.Stop();
			ItemSelection selectionScript = selectedItem.GetComponent<ItemSelection>();
			selectionScript.selected = false;
			Debug.Log("deselected item: " + selectedItem.name);
		}

		if (newlySelectedItem != selectedItem) {
			ItemSelection selectionScript;
			if( selectedItem != null ) {
				selectionScript = selectedItem.GetComponent<ItemSelection>();
			}
			selectedItem = newlySelectedItem;
			selectionScript = selectedItem.GetComponent<ItemSelection>();
			selectionScript.selected = true;
			selectedItem.particleSystem.Play();
			Debug.Log("newly selected item: " + newlySelectedItem.name);
		} else {
			selectedItem = null;
		}

	}

	void translateItem() {
		Frame currentFrame = controller.Frame ();
		Frame oldFrame = controller.Frame (1);

		Vector translation = currentFrame.Translation (oldFrame);
		Vector3 unityTranslationVector = calculateUnityTranslationVector (translation);
		selectedItem.GetComponent<TransformationManager>().translate(unityTranslationVector);
	}

	void rotateItem() {
		Frame currentFrame = controller.Frame ();
		Frame prevFrame = controller.Frame (5);
		float leapRotationAngle = currentFrame.RotationAngle(prevFrame, Vector.YAxis); 

		float unityRotationAngle = leapRotationAngle * (180f/Mathf.PI);

		selectedItem.GetComponent<TransformationManager>().rotate(unityRotationAngle);
	}

	void scaleItem() {
		Frame currentFrame = controller.Frame ();
		Frame prevFrame = controller.Frame (5);
		float leapScaleFactor = 0.001f*(currentFrame.ScaleFactor(prevFrame)-1f);
		Vector3 scalingVector = new Vector3(leapScaleFactor, leapScaleFactor, leapScaleFactor);

		selectedItem.GetComponent<TransformationManager>().scale(leapScaleFactor);
	}

	Vector3 calculateUnityTranslationVector(Vector vec) {
		float movementIncrementX = vec.x != 0 && Mathf.Abs(vec.x) > 5 ? 0.01f : 0;
		float movementIncrementY = vec.y != 0 && Mathf.Abs (vec.y) > 5 ? 0.01f : 0;
		float movementIncrementZ = vec.z != 0 && Mathf.Abs(vec.z) > 5 ? 0.01f : 0;
		float xDirection = vec.x >= 0 ? 1f : -1f;
		float yDirection = vec.y >= 0 ? 1f : -1f;
		float zDirection = vec.z >= 0 ? 1f : -1f;

		Vector3 unityVector = new Vector3 (xDirection * movementIncrementX,
		                                  yDirection * movementIncrementY,
		                                  zDirection * movementIncrementZ);

		return unityVector;
	}

	float calculateUnityRotationAngle(float leapRotationAngle) {
		return 0f;
	}

	public bool isItemSelected(GameObject obj) {
		return selectedItem == obj;
	}

}

