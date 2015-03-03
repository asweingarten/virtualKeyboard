using UnityEngine;
using System.Collections;
using Leap;

public class BallCursor : MonoBehaviour
{
	Controller controller;
	public float sensitivity = 50f;
	public float noiseThreshold = 2f;
	// Use this for initialization
	void Start ()
	{
		controller = new Controller();
	}

	// Update is called once per frame
	void Update ()
	{
		Frame currentFrame = controller.Frame();
		Frame previousFrame = controller.Frame (1);

		Vector leapTranslation = currentFrame.Translation(previousFrame);
		Vector3 unityTranslation = calculateUnityTranslationVector(leapTranslation);

		this.transform.Translate(unityTranslation);
	}

	Vector3 calculateUnityTranslationVector(Vector vec) {
		float movementIncrementX = vec.x != 0 && Mathf.Abs(vec.x) > noiseThreshold 
			? 0.001f*sensitivity 
			: 0;
		float movementIncrementY = vec.y != 0 && Mathf.Abs (vec.y) > noiseThreshold 
			? 0.001f*sensitivity 
			: 0;
		float movementIncrementZ = vec.z != 0 && Mathf.Abs(vec.z) > noiseThreshold
			? 0.001f*sensitivity 
			: 0;
		float xDirection = vec.x >= 0 ? 1f : -1f;
		float yDirection = vec.y >= 0 ? 1f : -1f;
		float zDirection = vec.z <= 0 ? 1f : -1f;
		
		Vector3 unityVector = new Vector3 (xDirection * movementIncrementX,
		                                   yDirection * movementIncrementY,
		                                   zDirection * movementIncrementZ);
		
		return unityVector;
	}
}

