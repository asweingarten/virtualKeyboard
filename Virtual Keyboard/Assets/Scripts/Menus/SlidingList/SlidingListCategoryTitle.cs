using UnityEngine;
using System.Collections;

public abstract class CategoryTitle : MonoBehaviour{
	public abstract void updateTitleText (string newTitle);
}

public class SlidingListCategoryTitle : CategoryTitle {

	private TextMesh textMesh;
	void Awake () {
		textMesh = gameObject.GetComponentInChildren<TextMesh> ();
		if (textMesh == null) {
			Debug.LogWarning("No text mesh for Catagory Title");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void updateTitleText(string newTitle) {
		textMesh.text = newTitle;
	}


}
