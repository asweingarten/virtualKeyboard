using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SlidingList))]
public abstract class SlidingListInteractionManager : MonoBehaviour {

	public delegate void PrevCategory();
	public static event PrevCategory OnPrevCategory;

	public delegate void NextCategory();
	public static event NextCategory OnNextCategory;

	public delegate void PrevListItem();
	public static event PrevListItem OnPrevListItem;

	public delegate void NextListItem();
	public static event NextListItem OnNextListItem;

	public delegate void CategoryListView();
	public static event CategoryListView OnCategoryListView;

	public delegate void SelectionBoxChosen();
	public static event SelectionBoxChosen OnSelectionBoxChosen;
	
	private SlidingList slidingList;

	void Awake () {
		slidingList = GetComponent<SlidingList> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void prevCatagory() {
		if (OnPrevCategory != null) {
			OnPrevCategory();
		}
	}

	protected void nextCategory() {
		if (OnNextCategory != null) {
			OnNextCategory();
		}
	}
	

	protected void prevListItem() {
		if (OnPrevListItem != null) {
			OnPrevListItem();
		}
	}

	protected void nextListItem() {
		if( OnNextListItem != null ) {
			OnNextListItem();
		}
	}

	protected void categoryListView() {
		if( OnCategoryListView != null ) {
			OnCategoryListView();
		}
	}

	protected void selectionBoxChosen() {
		if(OnSelectionBoxChosen != null ) {
			OnSelectionBoxChosen();
		}
	}
}
