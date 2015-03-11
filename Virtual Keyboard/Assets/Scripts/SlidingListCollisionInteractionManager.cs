using UnityEngine;
using System.Collections;

public class SlidingListCollisionInteractionManager : SlidingListInteractionManager {

	public float repeatedInteractionDelay = 0.5f;

	public GameObject prevCategoryTrigger;
	public GameObject nextCategoryTrigger;
	public GameObject prevItemTrigger;
	public GameObject nextItemTrigger;
	public GameObject categoryListTrigger;
	
	void Awake () {
	}

	// Use this for initialization
	void Start () {
		prepareInteractionBehaviours ();
		SlidingListInteractionOnCollision.OnSelect += handleInteraction;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy() {
		SlidingListInteractionOnCollision.OnSelect -= handleInteraction;
	}

	void handleInteraction ( GameObject interactionObject ) {
		if( interactionObject == prevCategoryTrigger ) {
			prevCatagory();
		} else if ( interactionObject == nextCategoryTrigger ) {
			nextCategory();
		} else if ( interactionObject == prevItemTrigger ) {
			prevListItem();
		} else if ( interactionObject == nextItemTrigger ) {
			nextListItem();
		} else if ( interactionObject == categoryListTrigger ) {
			categoryListView();
		}
	}

	void prepareInteractionBehaviours () {
		attachInteractionBehaviour (prevCategoryTrigger);
		attachInteractionBehaviour (nextCategoryTrigger);
		attachInteractionBehaviour (prevItemTrigger);
		attachInteractionBehaviour (nextItemTrigger);
		attachInteractionBehaviour (categoryListTrigger);
	}

	void attachInteractionBehaviour ( GameObject interactionObject ) {
		if( interactionObject.GetComponent<SlidingListInteractionOnCollision> () == null ) {
			interactionObject.AddComponent<SlidingListInteractionOnCollision> ();
		}
	}
}
