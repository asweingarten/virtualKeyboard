using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class textInput : MonoBehaviour {
	
	private float mouseXDelta;
	private float mouseYDelta;
	
	public float rotationY = 0f;
	public float rotationX = 0f;
	public float viewRange = 90.0f;
	
	public float mouseSensitivity = 10.0f;
	
	public GameObject thingy;

	GameObject userAndKeyboard;

	// Use this for initialization
	void Start () {

	}

	void HandleErrorEvent (object sender, EventArgs e)
	{
		Debug.Log(String.Format("DANGER, DANGER WILL ROBINSON: {0}", Input.mousePosition.y));
	}
	
	// Update is called once per frame
	void Update () {
		rotationX += Input.GetAxis ("Mouse X") * 1;

		string[] keys = {"`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=",
						"q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\",
						"a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "'",
						"z", "x", "c", "v", "b", "n", "m", ",", ".", "/",
						"space"};
		foreach (string s in keys){
			var tryToFindTextMesh = GameObject.Find ("key_" + s);
			var tm = (TextMesh)tryToFindTextMesh.GetComponent(typeof(TextMesh));
			if (Input.GetKeyDown (s)) {
				tm.color = new Color(100,0,0);
			}
			if (Input.GetKeyUp (s)) {
				tm.color = new Color(0,100,0);
			}
		}
	}
}
