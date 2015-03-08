using UnityEngine;
using System.Collections;

public class ButtonItem : MonoBehaviour {

	private bool isActive;
	public bool IsActive{
		get{
			return isActive;
		}
		set{
			isActive = value;
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (isActive) {
			Debug.Log ("INTERACTING WITH LIST ITEM");
			execute ();
		}
	}

	// Use this for initialization
	void Start ()
	{
		isActive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void execute() {
		SlidingList sl = transform.root.GetComponent ("SlidingList") as SlidingList;
		switch (transform.name) {
		case "UpArrow":
			sl.handleUpArrow();
			break;
		case "DownArrow":
			sl.handleDownArrow();
			break;
		case "LeftArrow":
			sl.handleLeftArrow();
			break;
		case "RightArrow":
			sl.handleRightArrow();
			break;
		default:
			Debug.Log (transform.name);
			break;
		}
	}
}
