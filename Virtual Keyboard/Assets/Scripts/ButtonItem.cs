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
			sl.scroll();
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
			sl.updateCategoryList ();
			break;
		default:
			Debug.Log (transform.name);
			break;
		}
	}
}
