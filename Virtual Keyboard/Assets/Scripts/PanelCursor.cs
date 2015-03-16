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

	private float prevXYMagnitude = 0f;
	private float speedModifier = 1f;
	public float pointerAcceleration = 0.1f;
	public float maxSpeedModifier = 2f;
	public float minSpeedModifier = 0.5f;
	private bool trackingPaused = false;

	public Material handOpenedCursor;
	public Material handHalfClosedCursor;
	public Material handClosedCursor;

	void Awake() {
		if(renderer != null) {
			renderer.material = handOpenedCursor;
		}
	}

	// Use this for initialization
	void Start ()
	{
		controller = new Controller();
		//controller.SetPolicy(Controller.PolicyFlag.POLICY_OPTIMIZE_HMD);
		// Translate the cursor to the center of the keyboard
		transform.localPosition = new Vector3(0,0,0);

		LeapGestures.HandClosedGestureTriggered += onHandClosed;
		LeapGestures.HandHalfClosedGestureTriggered += onHandHalfClosed;
		LeapGestures.HandOpenedGestureTriggered += onHandOpened;

		// Add support for a HandClosed gesture where the interaction panel is triggered
		LeapGestures.HandClosedGestureTriggered += HandClosedGestureTriggered;
	}

	// Update is called once per frame
	void Update ()
	{
		// Each frame, check the change in hand position from the last frame. Compute how much to move the cursor with this.
		Frame currentFrame = controller.Frame();
		Frame previousFrame = controller.Frame (1);

		if (interactionPanel.enabled == true && !trackingPaused) {
			Vector leapTranslation = currentFrame.Translation(previousFrame);
			Vector3 unityTranslation = calculateUnityTranslationVector(leapTranslation);
			TranslateCursor(unityTranslation);
		}
	}

	void onHandClosed(object sender, System.EventArgs e) {
		if(renderer != null) {
			renderer.material = handClosedCursor;
		}
		trackingPaused = true;
	}

	void onHandHalfClosed(object sender, System.EventArgs e) {
		if(renderer != null) {
			renderer.material = handHalfClosedCursor;
		}
		trackingPaused = false;
	}

	void onHandOpened(object sender, System.EventArgs e) {
		if(renderer != null) {
			renderer.material = handOpenedCursor;
		}
		trackingPaused = false;
		//StartCoroutine(pauseHandTracking(0.5f));
	}

	IEnumerator pauseHandTracking(float pauseTime) {
		trackingPaused = true;
		yield return new WaitForSeconds(pauseTime);
		trackingPaused = false;
	}

	// Calculate how much the cursor should move, based on how much the user moved their hands.
	Vector3 calculateUnityTranslationVector(Vector vec) {
		float magnitude = vec.Magnitude;
		if( magnitude > noiseThreshold ) {
			float xyMagnitude = Mathf.Abs(vec.y)+Mathf.Abs(vec.x);
			float xRatio = vec.x/xyMagnitude;
			float yRatio = vec.y/xyMagnitude;

			if( prevXYMagnitude < xyMagnitude && speedModifier < maxSpeedModifier) {
				speedModifier = Mathf.Min(speedModifier+pointerAcceleration, maxSpeedModifier);
			} else if(speedModifier > minSpeedModifier){
				speedModifier = Mathf.Max (speedModifier - pointerAcceleration, minSpeedModifier);
			}
			prevXYMagnitude = xyMagnitude;
			float movementIncrementX = speedModifier*xRatio*0.001f*sensitivityX;
			float movementIncrementY = speedModifier*yRatio*0.001f*sensitivityY;
			return new Vector3 (movementIncrementX, movementIncrementY, 0);
			
		} else {
			speedModifier = 1;
			prevXYMagnitude = 0;
			return new Vector3(0,0,0);
		}
	}

	// Helper function to translate the cursor by a position determined by the leap motion
	static int outOfBoundsCount = 0;
	void TranslateCursor(Vector3 translation) {

 		// Calculate cursor size and the position of the cursor
		Vector3 cursorPosition = transform.position; 

		Vector3 nextLocation = cursorPosition + transform.InverseTransformVector(translation);

		if (interactionPanel.withinBounds(nextLocation)) {
			//transform.Translate(translation, interactionPanel.transform.parent);
			transform.position = nextLocation;

			if(!interactionPanel.withinBounds(transform.position)) {
				transform.position = cursorPosition;
				//Debug.Log ("translation undone");
			}

		} else {
			transform.localPosition = new Vector3(0,0,0);
			//Debug.Log ("Out of bounds");
		}
		
	}

	// When a key tap gesture is triggered, call the interaction panel to trigger its action
	void HandClosedGestureTriggered(object sender, System.EventArgs e) {
	}



}

