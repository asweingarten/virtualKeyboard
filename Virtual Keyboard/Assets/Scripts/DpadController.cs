using UnityEngine;
using System.Collections;
using Leap;

public class DpadController : MonoBehaviour
{
	private OVRPlayerController playerController;
	public GameObject user = null;

	public delegate void LeapRotateOn(KeyCode keyId);
	public delegate void LeapRotateOff(KeyCode keyId);
	public static event LeapRotateOn OnLeapRotateOn;
	public static event LeapRotateOff OnLeapRotateOff;

	Controller controller = new Controller();
	Frame referenceFrame = null;
	bool activeX = false;
	bool activeZ = false;
	
	// Use this for initialization
	void Start () {
		if (user == null) return;
		playerController = user.GetComponent<OVRPlayerController>() as OVRPlayerController;

		setReferenceFrame();
	}
	
	// Update is called once per frame
	void Update () {
		if (referenceFrame == null) {
			bool success = setReferenceFrame();
			if (!success) return;
		}
		Frame currentFrame = controller.Frame ();
		
		float rotationAngleX = (Mathf.Rad2Deg)*currentFrame.RotationAngle(referenceFrame, Vector.XAxis);
		float rotationAngleZ = (Mathf.Rad2Deg)*currentFrame.RotationAngle(referenceFrame, Vector.ZAxis);

		KeyCode keyidX = rotationAngleX > 0 ? KeyCode.W : KeyCode.S;
		if (Mathf.Abs(rotationAngleX) >= 45) {
			OnLeapRotateOn(keyidX);
			triggerMovement(keyidX.ToString());
		} else {
			OnLeapRotateOff(keyidX);
			triggerStop(keyidX.ToString());
		}

		KeyCode keyidZ = rotationAngleZ > 0 ? KeyCode.D : KeyCode.A;
		if (Mathf.Abs(rotationAngleZ) >= 45) {
			OnLeapRotateOn(keyidZ);
			triggerMovement(keyidZ.ToString());
		} else {
			OnLeapRotateOff(keyidZ);
			triggerStop(keyidZ.ToString());
		}
		
	}
	
	// Returns true if reference frame set
	bool setReferenceFrame() {
		if (!controller.Frame().Hands.IsEmpty) {
			referenceFrame = controller.Frame();
		}
		return referenceFrame != null;
	}

	void triggerStop(string keyId) {
		string upperKeyId = keyId.ToUpper ();
		if (upperKeyId.Equals (KeyCode.W.ToString ())) {
			playerController.moveForward = false;
		} else if (upperKeyId.Equals (KeyCode.A.ToString ())) {
			playerController.moveLeft = false;
		} else if ( upperKeyId.Equals (KeyCode.D.ToString())) {
			playerController.moveRight = false;
		} else if ( upperKeyId.Equals (KeyCode.S.ToString())) {
			playerController.moveBack = false;
		}
	}
	
	void triggerMovement(string keyId) {
		string upperKeyId = keyId.ToUpper ();
		if (upperKeyId.Equals (KeyCode.W.ToString ())) {
			playerController.moveForward = true;
		} else if (upperKeyId.Equals (KeyCode.A.ToString ())) {
			playerController.moveLeft = true;
		} else if ( upperKeyId.Equals (KeyCode.D.ToString())) {
			playerController.moveRight = true;
		} else if ( upperKeyId.Equals (KeyCode.S.ToString())) {
			playerController.moveBack = true;
		}
	}
}


