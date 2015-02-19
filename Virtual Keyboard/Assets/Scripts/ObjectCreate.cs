using UnityEngine;
using System.Collections;

public class ObjectCreate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LeapGestures.CircularGestureTriggered += HandleCircularGestureTriggered;
	}
	
	void HandleCircularGestureTriggered (object sender, System.EventArgs e)
	{
		Debug.Log ("We're creating an object!!");
		GameObject gestureCreatedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		gestureCreatedObject.transform.SetParent (gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
