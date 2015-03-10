using UnityEngine;
using System.Collections;
using Leap;

public class PanelCursor : MonoBehaviour
{
	public InteractionPanel interactionPanel;

	// Set up sensitivity and noise threshold values. These can be customized in the editor
	public float sensitivityX = 15f;
	public float sensitivityY = 12f;
	public float sensitivityZ = 0f;
	public float noiseThreshold = 2f;

	private Controller controller;
	private BoxCollider collider;
	private Vector3 minBounds = Vector3.zero;
	private Vector3 maxBounds = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		controller = new Controller();

		// Add collision properties to the object so it can trigger collision events with interactible items
		collider = gameObject.AddComponent<BoxCollider>();
		Rigidbody rigidBody = gameObject.AddComponent<Rigidbody>(); 
		rigidBody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ 
							  | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

		// Transform the cursor to be right on top of the interation panel
		//TODO: Get an appropriate size of the cursor (equal to the smallest element in the interaction panel maybe?)
		this.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
		this.transform.position = interactionPanel.transform.position;
		this.transform.rotation = interactionPanel.transform.rotation;
		this.transform.parent = interactionPanel.transform.parent;
		

		calculateInteractionPanelBounds();
		// TODO: Determine a general way to translate the object so it's just imposed in the interactionPanel
		this.transform.Translate(0, 0, -0.035f, this.transform.parent);

		// Add support for a HandClosed gesture where the interaction panel is triggered
		LeapGestures.HandClosedGestureTriggered += HandClosedGestureTriggered;
	}

	// Update is called once per frame
	void Update ()
	{
		// Each frame, check the change in hand position from the last frame. Compute how much to move the cursor with this.
		Frame currentFrame = controller.Frame();
		Frame previousFrame = controller.Frame (1);

		if (interactionPanel.isActiveAndEnabled == true) {
			Vector leapTranslation = currentFrame.Translation(previousFrame);
			Vector3 unityTranslation = calculateUnityTranslationVector(leapTranslation);
			this.transform.Translate(unityTranslation, this.transform.parent);
			Debug.Log("Cursor coordinates: " + this.transform.localPosition);
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
		
		Vector3 unityVector = new Vector3 (xDirection * movementIncrementX, yDirection * movementIncrementY, zDirection * movementIncrementZ);
		
		return unityVector;
	}

	// When a key tap gesture is triggered, call the interaction panel to trigger its action
	void HandClosedGestureTriggered(object sender, System.EventArgs e) {
		interactionPanel.TriggerAction();
	}

	//TODO: Determine bounds of containing interactionPanel, and don't let the cursor go outside of those bounds.
	void calculateInteractionPanelBounds() {
		minBounds = new Vector3(1000, 1000, 1000);
		maxBounds = new Vector3(-1000, -1000, -1000);

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

					Debug.Log("Comparing bounds. Min: " + min + " , Max: " + max);
				}
	        }
	             
	        minBounds = this.transform.TransformPoint(minBounds);
	        maxBounds = this.transform.TransformPoint(maxBounds);
			Debug.Log("Computed new bounds. Min: " + minBounds + " , Max: " + maxBounds);
		}
	}

}

