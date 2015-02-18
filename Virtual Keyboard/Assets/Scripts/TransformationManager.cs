using UnityEngine;
using System.Collections;

public class TransformationManager : MonoBehaviour {

	public bool canTranslateX = false;
	public bool canTranslateY = false;
	public bool canTranslateZ = false;

	public bool canRotateX = false;
	public bool canRotateY = false;
	public bool canRotateZ = false;

	public bool canScaleX = false;
	public bool canScaleY = false;
	public bool canScaleZ = false;

	// Use this for initialization
	void Start () {
	
	}

	public void translate(Vector3 translationVector) {
		if (!canTranslateX) {
			translationVector.x = 0;
		}
		if (!canTranslateY) {
			translationVector.y = 0;
		}
		if (!canTranslateZ) {
			translationVector.z = 0;
		}

		this.transform.Translate(translationVector);
	}
	
	// float rotationAngle: is in degrees
	public void rotate(float rotationAngle) {
		Vector3 rotationVector = new Vector3();
		rotationVector.x = canRotateX ? rotationAngle : 0;
		rotationVector.y = canRotateY ? rotationAngle : 0;
		rotationVector.z = canRotateZ ? rotationAngle : 0;
				
		this.transform.Rotate(rotationVector);
	}

	public void scale(float scaleValue){
		Vector3 scaleVector = new Vector3();
		scaleVector.x = canScaleX ? scaleValue : 0;
		scaleVector.y = canScaleX ? scaleValue : 0;
		scaleVector.z = canScaleZ ? scaleValue : 0;
		this.transform.localScale += scaleVector;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
