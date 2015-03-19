using UnityEngine;
using System.Collections;
using Leap;

public class LeapCursor : HandModel
{
	public GameObject cursor;

	public delegate void FingerCountChangeAction(int fingerCount);
	public static event FingerCountChangeAction OnFingerCountChanged;

	private int fingerCount;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{

	}

	//Finger Count without thumb
	public int getFingerCount() {
		FingerList fingerList = controller_.GetFrame().Fingers.Extended();
		int count = 0;
		foreach( Finger finger in fingerList ) {
			if( finger.Type() != Leap.Finger.FingerType.TYPE_THUMB ) {
				count++;
			}
		}
		return count;
	}

	public override void InitHand ()
	{
		SetPositions();
	}

	public override void UpdateHand() {
		SetPositions();
		if( OnFingerCountChanged != null ) {
			int count = getFingerCount();
			if( count != fingerCount ) {
				fingerCount = count;
				OnFingerCountChanged( fingerCount );
			}
		}

	}

	protected virtual void SetPositions() {
		if( cursor != null ) {
			cursor.transform.position = GetPalmPosition();
		}
	}
}

