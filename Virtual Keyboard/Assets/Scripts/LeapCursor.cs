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

	//Finger Count without thumb
	public void getFingerCount() {
		FingerList fingerList = controller_.GetFrame().Fingers.Extended();
		int count = 0;
		foreach( Finger finger in fingerList ) {
			if( !finger.Type == Finger.FingerType.TYPE_THUMB ) {
				count++;
			}
			return count;
		}
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

