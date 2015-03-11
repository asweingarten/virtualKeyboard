using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListManager : MonoBehaviour {

	private float listItemHeight = 0.1f;
	private float listItemSpacing = 0.05f;
	private List<GameObject> catagories;
	private GameObject catagoryList;

	private int currentCatagory = 0;

	void Awake () {
		catagories = new List<GameObject> ();
	}

	// Use this for initialization
	void Start () {
		findCatagories ();
		if( catagories.Count > 1 ) {
			Debug.Log ("CreateCategoryList");
			createCatagoryList ();
		} else if( catagories.Count == 1 ) {
			catagories[0].SetActive(true);
		} else {
			//If list has no entries destroy it
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void createCategoryListEntries() {
		int itemCount = 0;
		foreach( GameObject catagory in catagories ) {
			GameObject catagoryEntry = GameObject.CreatePrimitive(PrimitiveType.Cube);
			catagoryEntry.name = catagory.name;//Name of entry inheirits from catagory name
			catagoryEntry.transform.SetParent( catagoryList.transform );//CatagoryEntry is child of catagory list
			catagoryEntry.tag = "ListItem";
			Transform entryTransform = catagoryEntry.transform;

			//Note Positioning of these elements is handled In Category component
			entryTransform.localScale = new Vector3( 1f, 1f, listItemHeight );
			entryTransform.localRotation = Quaternion.Euler(Vector3.zero);

			GameObject catagoryText = new GameObject();
			catagoryText.transform.SetParent(catagoryEntry.transform);
			TextMesh textMesh = catagoryText.AddComponent<TextMesh>();
			textMesh.text = catagory.name;
			textMesh.characterSize = 0.045f;
			textMesh.fontSize = 20;
			textMesh.anchor = TextAnchor.MiddleCenter;
			textMesh.font = textMesh.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			catagoryText.GetComponent<MeshRenderer>().material = textMesh.font.material;
			catagoryText.transform.localPosition = Vector3.zero;
			catagoryText.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
			catagoryText.transform.localScale = new Vector3( 1f, 10f, 1f );

			++itemCount;
		}
	}

	private bool isCatagoryListCurrent () {
		if( catagoryList == null ) return false;
		List<string> catagoryNames = new List<string> ();
		foreach( GameObject catagory in catagories ) {
			catagoryNames.Add(catagory.name);
		}
		int childCount = catagoryList.transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject catagoryEntry = catagoryList.transform.GetChild(i).gameObject;
			if(catagoryNames.Contains(catagoryEntry.name)) {
				//Remove matching catagory name
				catagoryNames.Remove(catagoryEntry.name);
			} else {
				//If our catagoryList object has entries for non exisitent catagories it is not up to date
				return false;
			}
		}
		//Our catagory object is current if all catagories names have been removed
		return catagoryNames.Count == 0;
	}

	private void createCatagoryList() {
		if( isCatagoryListCurrent() ) return;//update to not create if exisiting catagory list is up to date
		if( catagoryList != null ) Destroy(catagoryList);//Destroy Prev catagoryList if it exists

		catagoryList = new GameObject ();
		catagoryList.transform.SetParent (transform);
		catagoryList.name = "CatagoryList";
		catagoryList.tag = "CatagoryList";
		catagoryList.transform.localScale = Vector3.one;
		catagoryList.transform.localPosition = Vector3.zero;
		catagoryList.transform.localRotation = Quaternion.Euler (Vector3.zero); 
		createCategoryListEntries ();

		//Add Category Component to handle list item positioning and scrolling
		Catagory cat = catagoryList.AddComponent<Catagory> ();
		cat.title = "CatagoryList";
	}

	//Initialize list of catagories and find catagoryList
	private void findCatagories() {
		int childCount = transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject child = transform.GetChild(i).gameObject;
			Debug.Log(child.name);
			if( child.tag.Equals("Catagory") ) {
				catagories.Add(child);
				child.SetActive(false);//Start with all catagories inactive
			} else if ( child.tag.Equals("CatagoryList") ) {
				Debug.Log ("Found Catagory List");
				catagoryList = child;
			}
		}
	}

	void addCatagoryEntry(string catagoryName, GameObject catagoryObject) {
	}

	void updateCatagoryList() {
	}

	private void deactivateAllCatagories() {
		foreach( GameObject catagory in catagories ) {
			catagory.SetActive(false);
		}
	}

	//Assumes catagoryList not null and catagories not empty
	public void returnToLastActiveCatagory() {
		catagoryList.SetActive(false);
		catagories [currentCatagory].SetActive (true);
	}

	public void nextCatagory() {
		//In the event catagoryList is active return to last active catagory
		if (catagoryList != null && catagoryList.activeSelf == true) {
			returnToLastActiveCatagory();
		} else {
			catagories [currentCatagory].SetActive (false);
			currentCatagory = (currentCatagory + 1) % catagories.Count;
			catagories [currentCatagory].SetActive (true);
		}

	}

	public void prevCatagory() {
		//In the event catagoryList is active return to last active catagory
		if (catagoryList != null && catagoryList.activeSelf == true) {
			returnToLastActiveCatagory();
		} else {
			catagories [currentCatagory].SetActive (false);
			currentCatagory = (currentCatagory - 1) % catagories.Count;
			catagories [currentCatagory].SetActive (true);
		}

	}

	public void catagoryView() {
		catagories [currentCatagory].SetActive (false);
		catagoryList.SetActive (true);
	}

	public void scrollUp () {
	}

	public void scrollDown () {
	}

}
