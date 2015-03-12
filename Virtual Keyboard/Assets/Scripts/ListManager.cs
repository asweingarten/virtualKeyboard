using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListManager : MonoBehaviour {
	private List<GameObject> itemList;
	private float epsilon = 0.0001f;//Smudge factor when comparing float values

	private float height = 0.1f;
	public float listItemHeight {
		get {
			return height;
		}
		set {
			height = value;
			setListItemHeights();
			posistionListItems();
		}
	}

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
		Debug.Log (gameObject.name);
		itemList = new List<GameObject> ();

	}

	// Use this for initialization
	void Start () {
		transform.localPosition = Vector3.zero;
		getListItems ();
		setListItemHeights ();
		posistionListItems ();
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

	private Vector3 calculateItemPosistion (int itemNumber, GameObject item, bool posistionBelow=true) {
		float itemHeight = item.transform.localScale.z;
		float height = itemNumber * (itemHeight + itemSpacing);
		//if positioning element below last substract height otherwise add it
		if (posistionBelow) height = -height;
		Vector3 pos = new Vector3( 0f, 0f, height);
		return pos;
	}

	public void positionItemAbove (int itemCount, GameObject item) {
		Debug.Log (gameObject.name + " positionItemAbove" + itemCount);
		item.transform.localPosition = calculateItemPosistion( itemCount, item, false );
		//Check if item is above or below list bounds
		if (isItemBelowList(item) || isItemAboveList(item)) {
			item.SetActive(false);
		} else {
			item.SetActive(true);
		}
	}
	
	public void positionItemBelow (int itemCount, GameObject item) {
		Debug.Log (gameObject.name + " positionItemBelow" + itemCount );
		item.transform.localPosition = calculateItemPosistion( itemCount, item, true );
		//Check if item is above or below list bounds
		if (isItemBelowList(item) || isItemAboveList(item)) {
			item.SetActive(false);
		} else {
			item.SetActive(true);
		}
	}
	
	void posistionListItems () {
		int itemCount = 0;
		for( int i = 0; i < itemList.Count; i++ ) {
			Debug.Log(gameObject.name + " Pos listItem");
			GameObject nextFromStart = itemList[i];
			if( i != 0 && itemList.Count - i < i) break;
			positionItemBelow( itemCount, nextFromStart );
			if( i != 0  && itemList.Count - i != i ) {
				GameObject nextFromEnd = itemList[itemList.Count - i];
				positionItemAbove( itemCount, nextFromEnd );
			}
			itemCount++;
		}
	}

	void setListItemHeights () {
		foreach( GameObject item in itemList ) {
			item.transform.localScale = new Vector3( 1f, 1f, listItemHeight );
		}
	}

	void getListItems () {
		int childCount = transform.childCount;
		Debug.Log (gameObject.name + " getListItems: " + childCount);
		for( int i = 0; i < childCount; i++ ) {
			GameObject child = transform.GetChild(i).gameObject;
			if( child.tag.Equals("ListItem") ) {
				Debug.Log ("Add list item");
				itemList.Add( child );
			}
		}
	}

	void OnValidate () {
		gameObject.name = categoryTitle;
	}
	
	public void nextListItem () {
		bool firstActiveState = false;
		Vector3 firstPos = Vector3.zero;
		for( int i = itemList.Count - 1; i >= 0 ; i-- ) {
			GameObject currentItem = itemList[i];
			GameObject nextItem = itemList[(i+1) % itemList.Count];
			if( i == itemList.Count - 1 ) {
				firstActiveState = nextItem.activeSelf;
				firstPos = nextItem.transform.localPosition;
				nextItem.SetActive(currentItem.activeSelf);
				nextItem.transform.localPosition = currentItem.transform.localPosition;
			} else if(i == 0) {
				nextItem.SetActive(firstActiveState);
				nextItem.transform.localPosition = firstPos;
			} else {
				nextItem.SetActive(currentItem.activeSelf);
				nextItem.transform.localPosition = currentItem.transform.localPosition;
			}
		}
	}
	
	public void prevListItem () {
		bool firstActiveState = false;
		Vector3 firstPos = Vector3.zero;
		for( int i = 0; i < itemList.Count; i++ ) {
			GameObject prevItem = itemList[i];
			GameObject currentItem = itemList[(i+1) % itemList.Count];
			if( i == 0 ) {
				firstActiveState = prevItem.activeSelf;
				firstPos = prevItem.transform.localPosition;
				prevItem.SetActive(currentItem.activeSelf);
				prevItem.transform.localPosition = currentItem.transform.localPosition;
			} else if(i == itemList.Count - 1) {
				prevItem.SetActive(firstActiveState);
				prevItem.transform.localPosition = firstPos;
			} else {
				prevItem.SetActive(currentItem.activeSelf);
				prevItem.transform.localPosition = currentItem.transform.localPosition;
			}
		}
	}
}
