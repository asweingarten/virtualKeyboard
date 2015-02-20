using UnityEngine;
using System.Collections;

public class CollisionReporter : MonoBehaviour {

	public string message = "Hello World";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter () {
		Debug.Log ("Collision Enter: " + message);
	}

	void OnCollisionExit () {
		Debug.Log ("Collision Exit: " + message);
	}
}
