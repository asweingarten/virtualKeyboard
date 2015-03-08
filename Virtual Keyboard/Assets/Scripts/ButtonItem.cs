using UnityEngine;
using System.Collections;

public class ButtonItem : MonoBehaviour {
	
	void OnCollisionEnter(Collision collision) {
		execute ();
	}

	// Use this for initialization
	void Start ()
	{
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
		case "TitleBoxFrame":
			sl.updateCategoryList ();
			break;
		default:
			Debug.Log (transform.name);
			break;
		}
	}
}
