using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListManager : MonoBehaviour {
	private List<GameObject> itemList;
	private float epsilon = 0.0001f;//Smudge factor when comparing float values

	private float spacing = 0.05f;
	public float itemSpacing {
		get {
			return spacing;
		}
		set {
			spacing = value;
			posistionListItems();
		}
	}

	[SerializeField]
	private string categoryTitle = "Category";
	public string title {
		get {
			return categoryTitle;
		}
		set {
			categoryTitle = value;
			gameObject.name = categoryTitle;
		}
	}

	void Awake () {
		itemList = new List<GameObject> ();
		transform.localPosition = Vector3.zero;
		getListItems ();
		posistionListItems ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private bool isItemBelowList( GameObject item ) {
		Vector3 pos = item.transform.localPosition;
		if( transform.parent == null || transform.parent.gameObject.GetComponent<MeshFilter> () == null ) {
			return false;
		} else {
			Mesh mesh =  transform.parent.gameObject.GetComponent<MeshFilter> ().sharedMesh;
			Vector3 listBottom = mesh.bounds.min;
			Vector3 listCenter = mesh.bounds.center;

			return (listCenter.z + pos.z - (0.5 * item.transform.localScale.z) < listBottom.z - epsilon);	
		}

	}
	
	private bool isItemAboveList( GameObject item ) {
		Vector3 pos = item.transform.localPosition;
		if( transform.parent == null || transform.parent.gameObject.GetComponent<MeshFilter> () == null ) {
			return false;
		} else {
			Mesh mesh =  transform.parent.gameObject.GetComponent<MeshFilter> ().sharedMesh;
			Vector3 listTop = mesh.bounds.max;
			Vector3 listCenter = mesh.bounds.center;

			return (listCenter.z + pos.z - (0.5 * item.transform.localScale.z) > listTop.z + epsilon);	
		}
	}

	private Vector3 calculateItemPosistion (int itemNumber, GameObject item) {
		float itemHeight = item.transform.localScale.z;
		float height = itemNumber * (itemHeight + itemSpacing);
		Vector3 pos = new Vector3( 0f, 0f, -height);
		return pos;
	}

	void posistionListItems () {
		Debug.Log ("posistionListItems");
		int itemCount = 0;
		foreach( GameObject item in itemList ) {
			item.transform.localPosition = calculateItemPosistion( itemCount, item );

			//Check if item is above or below list bounds
			if (isItemBelowList(item) || isItemAboveList(item)) {
				item.SetActive(false);
			} else {
				item.SetActive(true);
			}
			itemCount++;
		}
	}

	void getListItems () {
		int childCount = transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject child = transform.GetChild(i).gameObject;
			if( child.tag.Contains("ListItem") ) {
				itemList.Add( child );
			}
		}
	}

	void OnValidate () {
		gameObject.name = categoryTitle;
	}
}
