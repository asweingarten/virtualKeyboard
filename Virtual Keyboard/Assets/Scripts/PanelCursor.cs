using UnityEngine;
using System.Collections;
using Leap;

public class PanelCursor : MonoBehaviour
{
	public InteractionPanel interactionPanel;

	[SerializeField]
	float sensitivityX = 15f;
	[SerializeField]
	float sensitivityY = 12f;
	[SerializeField]
	float sensitivityZ = 0f;
	[SerializeField]
	float noiseThreshold = 2f;

	private Controller controller;
	private BoxCollider collider;
	private Vector3 interactionPanelSize = Vector3.zero;
	private Vector3 interactionPanelCenter = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		controller = new Controller();

		// Translate the cursor to the center of the keyboard
		transform.localPosition = new Vector3(0,0,0);
		
		// Add support for a HandClosed gesture where the interaction panel is triggered
		LeapGestures.HandClosedGestureTriggered += HandClosedGestureTriggered;
	}

	// Update is called once per frame
	void Update ()
	{
		// Each frame, check the change in hand position from the last frame. Compute how much to move the cursor with this.
		Frame currentFrame = controller.Frame();
		Frame previousFrame = controller.Frame (1);

		if (interactionPanel.enabled == true) {
			Vector leapTranslation = currentFrame.Translation(previousFrame);
			Vector3 unityTranslation = calculateUnityTranslationVector(leapTranslation);
			TranslateCursor(unityTranslation);
		}
	}


	// Calculate how much the cursor should move, based on how much the user moved their hands.
	Vector3 calculateUnityTranslationVector(Vector vec) {
		float movementIncrementX = vec.x != 0 && Mathf.Abs(vec.x) > noiseThreshold 
			? 0.001f*sensitivityX
			: 0;
		float movementIncrementY = vec.y != 0 && Mathf.Abs (vec.y) > noiseThreshold 
			? 0.001f*sensitivityY
			: 0;
		float movementIncrementZ = vec.z != 0 && Mathf.Abs(vec.y) > noiseThreshold
			? 0.001f*sensitivityZ 
			: 0;
		float xDirection = vec.x >= 0 ? 1f : -1f;
		float yDirection = vec.y >= 0 ? 1f : -1f;
		float zDirection = vec.z <= 0 ? 1f : -1f;
		
		return new Vector3 (xDirection * movementIncrementX, yDirection * movementIncrementY, 0);
	}

	// Helper function to translate the cursor by a position determined by the leap motion
	void TranslateCursor(Vector3 translation) {
 		// Calculate cursor size and the position of the cursor
		Vector3 cursorPosition = this.transform.position; 

		Vector3 nextLocation = cursorPosition + translation;

		if (interactionPanel.withinBounds(nextLocation)) {
			this.transform.Translate(translation, interactionPanel.transform.parent);
		} else {
			Debug.Log ("Out of bounds");
		}
		
	}

	// When a key tap gesture is triggered, call the interaction panel to trigger its action
	void HandClosedGestureTriggered(object sender, System.EventArgs e) {
	}



}

