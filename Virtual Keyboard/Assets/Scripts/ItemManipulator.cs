using UnityEngine;
using System.Collections;
using Leap;

public class ItemManipulator : MonoBehaviour
{
	private GameObject selectedItem;

	private Controller controller;

	private bool trackingActive = false;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		ItemSelection.OnItemSelected += updateSelectedItem;
	}

	// Update is called once per frame
	void Update () {
		if (selectedItem == null) return;
		translateItem ();
		rotateItem();
		scaleItem();
	}

	void updateSelectedItem(GameObject newlySelectedItem) {
		Debug.Log("newly selected item: " + newlySelectedItem.name);
		selectedItem = newlySelectedItem;
	}

	void translateItem() {
		Frame currentFrame = controller.Frame ();
		Frame oldFrame = controller.Frame (1);

		Vector translation = currentFrame.Translation (oldFrame);
		Vector3 unityTranslationVector = calculateUnityTranslationVector (translation);
		selectedItem.GetComponent<TransformationManager>().translate(unityTranslationVector);
		// selectedItem.transform.Translate (unityTranslationVector);
	}

	void rotateItem() {
		Frame currentFrame = controller.Frame ();
		Frame prevFrame = controller.Frame (5);
		float leapRotationAngle = currentFrame.RotationAngle(prevFrame, Vector.YAxis); 

		float unityRotationAngle = leapRotationAngle * (180f/Mathf.PI);

		selectedItem.GetComponent<TransformationManager>().rotate(unityRotationAngle);
		// selectedItem.transform.Rotate(new Vector3(0f, unityRotationAngle, 0f));
	}

	void scaleItem() {
		Frame currentFrame = controller.Frame ();
		Frame prevFrame = controller.Frame (5);
		float leapScaleFactor = 0.001f*(currentFrame.ScaleFactor(prevFrame)-1f);
		Vector3 scalingVector = new Vector3(leapScaleFactor, leapScaleFactor, leapScaleFactor);

		selectedItem.GetComponent<TransformationManager>().scale(leapScaleFactor);
		// selectedItem.transform.localScale += (scalingVector);
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

}

