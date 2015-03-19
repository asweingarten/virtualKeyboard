using UnityEngine;
using System.Collections;
using Leap;

public class LeapCursor : HandModel
{
	public GameObject cursor;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public override void InitHand ()
	{
		SetPositions();
	}

	public override void UpdateHand() {
		SetPositions();
	}

	protected virtual void SetPositions() {
		if( cursor != null ) {
			cursor.transform.position = GetPalmPosition();
		}
	}

}

