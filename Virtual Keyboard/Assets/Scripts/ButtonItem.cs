using UnityEngine;
using System.Collections;

public class ButtonItem : MonoBehaviour {

	private float lastActiveTime;
	void OnCollisionEnter(Collision collision) {
		if( lastActiveTime != null && Time.time - lastActiveTime < .75f ) return;
		lastActiveTime = Time.time;
		Debug.Log ("Collision");
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
		SlidingList sl = transform.parent.parent.GetComponent<SlidingList> ();
		switch (transform.name) {
		case "UpArrow":
			sl.scrollListUp();
			break;
		case "DownArrow":
			sl.scrollListDown();
			break;
		case "LeftArrow":
			sl.scrollCategoriesLeft();
			break;
		case "RightArrow":
			sl.scrollCategoriesRight();
			break;
		case "TitleBoxFrame":
			if( sl == null ) break;
			sl.updateCategoryList ();
			break;
		default:
			Debug.Log (transform.name);
			break;
		}
	}
}
