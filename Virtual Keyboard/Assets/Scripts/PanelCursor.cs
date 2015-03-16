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
		transform.localPosition = new Vector3(3,0,5);
		
		calculateInteractionPanelBounds();

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
		
		return new Vector3 (xDirection * movementIncrementX, yDirection * movementIncrementY, zDirection * movementIncrementZ);
	}

	// Helper function to translate the cursor by a position determined by the leap motion
	void TranslateCursor(Vector3 translation) {
		// Calculate bounds of the interaction panel

		//****  ISSUES: ****
		//	- (0,0,0) is not the center of the keyboard to start. So automatically our bounds are going to be off
		//	- We also don't account for the transform of the keyboard when using the interactionPanelSize. 
		//	  Because of this the bounds are calculated in the wrong plane (keyboard is in x/z plane, size is x/y)
		//  - I used the world coordinates before because they don't worry about what plane we are in. 
		
		//**** SOLUTIONS? ****
		//	- If the keyboard prefab was correctly centered (or even better bottom left) on (0,0,0) this would be way easier
		//  - Also would be simpler if the prefab was in the x/y plane, which is where it is always used.

		//  - Alternatively more math can be done to try and figure this out. Need a mental break though.
		
		//interactionPanelCenter = interactionPanel.transform.TransformPoint(interactionPanelSize/2);
		//Vector3 minBounds =  interactionPanel.transform.TransformPoint(Vector3.zero);//interactionPanelCenter - interactionPanel.transform.TransformDirection(interactionPanelSize/2);
		//minBounds = interactionPanel.transform.parent.InverseTransformPoint(minBounds);
		//Vector3 maxBounds = interactionPanel.transform.TransformDirection(interactionPanelSize);//interactionPanelCenter + interactionPanel.transform.parent.TransformDirection(interactionPanelSize/2);
		//maxBounds = interactionPanel.transform.parent.InverseTransformPoint(maxBounds);
 

 		// Calculate cursor size and the position of the cursor
		Vector3 cursorPosition = this.transform.localPosition; //+ interactionPanel.transform.parent.TransformDirection(translation);
		//cursorPosition = interactionPanel.transform.InverseTransformPoint(cursorPosition);
		

		interactionPanelCenter = new Vector3(0,0,0);
		Vector3 minBounds =  interactionPanelCenter - interactionPanelSize/2;
		Vector3 maxBounds = interactionPanelCenter + interactionPanelSize/2;

		Vector3 minTest = cursorPosition - minBounds;
		Vector3 maxTest = maxBounds - cursorPosition;

		//if (minTest.x < 0 || minTest.y < 0 || minTest.z < 0 || maxTest.x < 0 || maxTest.y < 0 || maxTest.z < 0) {
		//	Debug.Log("Out of bounds: Mintest: " + minTest + ", Maxtest: " + maxTest);
		//} else {
			this.transform.Translate(translation, interactionPanel.transform.parent);
		//}
	}

	// When a key tap gesture is triggered, call the interaction panel to trigger its action
	void HandClosedGestureTriggered(object sender, System.EventArgs e) {
		interactionPanel.TriggerAction();
	}

	//TODO: Determine bounds of containing interactionPanel, and don't let the cursor go outside of those bounds.
	void calculateInteractionPanelBounds() {
		Vector3 minBounds = new Vector3(1000, 1000, 1000);
		Vector3 maxBounds = new Vector3(-1000, -1000, -1000);

		if (interactionPanel != null) {
			Collider[] childColliders = interactionPanel.GetComponentsInChildren<Collider>();
			foreach(Collider childCollider in childColliders) {
				if (childCollider != null) {
					Vector3 min = childCollider.bounds.min;
					Vector3 max = childCollider.bounds.max;

					minBounds.x = Mathf.Min(min.x, minBounds.x);
					minBounds.y = Mathf.Min(min.y, minBounds.y);
					minBounds.z = Mathf.Min(min.z, minBounds.z);

					maxBounds.x = Mathf.Max(max.x, maxBounds.x);
					maxBounds.y = Mathf.Max(max.y, maxBounds.y);
					maxBounds.z = Mathf.Max(max.z, maxBounds.z);
				}
	        }

	        interactionPanelSize = (maxBounds - minBounds);
	        Debug.Log("Panel Size: " + interactionPanelSize);
		}
	}

}

