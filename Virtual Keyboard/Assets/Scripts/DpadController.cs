using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof(InteractionPanel))]
public class DpadRotationControl : MonoBehaviour
{
	Controller controller = new Controller();
	Frame referenceFrame = null;
	bool activeX = false;
	bool activeZ = false;
	
	// Use this for initialization
	void Start () {
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
		
		if (Mathf.Abs(rotationAngleX) >= 45) {
			KeyCode keyid = rotationAngleX > 0 ? KeyCode.W : KeyCode.S;
			activeX = true;
			Debug.Log("X activation");
		} else if (activeX) {
			activeX = false;
		}
		
		if (Mathf.Abs(rotationAngleZ) >= 45) {
			KeyCode keyid = rotationAngleZ > 0 ? KeyCode.D : KeyCode.A;
			activeZ = true;
			Debug.Log ("Z activation");
		} else if(activeZ) {
			activeZ = false;
		}
		
	}
	
	// Returns true if reference frame set
	bool setReferenceFrame() {
		if (!controller.Frame().Hands.IsEmpty) {
			referenceFrame = controller.Frame();
		}
		return referenceFrame != null;
	}
}


