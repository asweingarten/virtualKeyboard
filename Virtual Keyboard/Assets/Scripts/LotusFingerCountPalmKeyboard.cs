using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

public class LotusFingerCountPalmKeyboard : MonoBehaviour {
	 
	public GameObject studyObject;
	public AudioSource clickSound = null;
	public TextReceiver textReceiver = null;
	private Controller controller;

	public LotusFingerCountPalmCluster topCluster;
	public LotusFingerCountPalmCluster topLeftCluster;
	public LotusFingerCountPalmCluster leftCluster;
	public LotusFingerCountPalmCluster bottomLeftCluster;
	public LotusFingerCountPalmCluster bottomCluster;
	public LotusFingerCountPalmCluster bottomRightCluster;
	public LotusFingerCountPalmCluster rightCluster;
	public LotusFingerCountPalmCluster topRightCluster;

	public float pitchTopThreshold = 0.8f;
	public float pitchBottomThreshold = -0.2f;
	public float yawThreshold = 0.4f;

	private enum PalmSelection { Center, Top, Bottom, Right, Left, TopLeft, TopRight, BottomLeft, BottomRight };
	private PalmSelection palmSelection = PalmSelection.Center;

	private int extendedFingerCount = 0;

	private List<LotusFingerCountPalmCluster> lotusClusters;

	void Awake() {
		//LotusClusterFingerCountBoundary.LotusClusterSelected += onLotusClusterSelected;
		controller = new Controller();
		lotusClusters = new List<LotusFingerCountPalmCluster>();
		lotusClusters.Add(topCluster);
		lotusClusters.Add(topLeftCluster);
		lotusClusters.Add(leftCluster);
		lotusClusters.Add(bottomLeftCluster);
		lotusClusters.Add(bottomCluster);
		lotusClusters.Add(bottomRightCluster);
		lotusClusters.Add(rightCluster);
		lotusClusters.Add(topRightCluster);
	}
	
	void Update() {
		Frame frame = controller.Frame();
		//To avoid unnecessary cluster updates first compare current to prev finger count
		int newExtendedFingerCount = getExtendedFingerCount(frame);
		if( newExtendedFingerCount != extendedFingerCount ) {
			extendedFingerCount = newExtendedFingerCount;
			updateActiveKeyIndicies();
		}

		PalmSelection newPalmSelection = getPalmSelection(frame);
		if( newPalmSelection != palmSelection ) {
			palmSelection = newPalmSelection;
			typeSelectedKey();
		}

		Debug.Log(palmSelection);
	}

	private void typeSelectedKey() {
		LotusFingerCountPalmCluster selectedCluster = getSelectedCluster();
		if( selectedCluster == null ) return;
		KeyActivator activeKey = selectedCluster.getActiveKey();
		typeText(activeKey.keyId);
	}

	private LotusFingerCountPalmCluster getSelectedCluster() {
		switch(palmSelection) {
			case PalmSelection.Top: 
				return topCluster; 
			case PalmSelection.TopLeft: 
				return topLeftCluster; 
			case PalmSelection.Left: 
				return leftCluster; 
			case PalmSelection.BottomLeft: 
				return bottomLeftCluster; 
			case PalmSelection.Bottom: 
				return bottomCluster; 
			case PalmSelection.BottomRight: 
				return bottomRightCluster; 
			case PalmSelection.Right: 
				return rightCluster; 
			case PalmSelection.TopRight: 
				return topRightCluster; 
		}
		return null;
	}

	private void updateActiveKeyIndicies() {
		//Keys are zero indexed and we want one finger to activate the 0th index
		int activeKeyIndex = extendedFingerCount - 1;
		foreach( var cluster in lotusClusters ) {
			cluster.setActiveKeyIndex(activeKeyIndex);
		}
	}

	private PalmSelection getPalmSelection(Frame frame) {
		Vector palmNormal = frame.Hands.Frontmost.PalmNormal;
		//Debug.Log ( palmNormal );

		//If the leap motion does not see any hands return center posisition
		if( frame.Hands.Count == 0 ) return PalmSelection.Center;

		PalmSelection selection = PalmSelection.Center;
		float 	x = palmNormal.x,//frame.Hands.Frontmost.Direction.Pitch, 
				y = palmNormal.y,//frame.Hands.Frontmost.Direction.Yaw, 
				z = palmNormal.z;//frame.Hands.Frontmost.Direction.Roll;

		float 	pitch = frame.Hands.Frontmost.Direction.Pitch,
				yaw = frame.Hands.Frontmost.Direction.Yaw,
		roll = frame.Hands.Frontmost.Direction.Roll;
		//Debug.Log( "Pitch: " + pitch + " Yaw: " + yaw + " Roll: " + roll );
		if( pitch > pitchTopThreshold ) {
			selection = PalmSelection.Top;
		} else if( pitch < pitchBottomThreshold ) {
			selection = PalmSelection.Bottom;
		}

		if( yaw < -yawThreshold ) {
			switch(selection) {
				case PalmSelection.Top: selection = PalmSelection.TopLeft; break;
				case PalmSelection.Bottom: selection = PalmSelection.BottomLeft; break;
				default: selection = PalmSelection.Left; break;
			}
		} else if( yaw > yawThreshold ) {
			switch(selection) {
				case PalmSelection.Top: selection = PalmSelection.TopRight; break;
				case PalmSelection.Bottom: selection = PalmSelection.BottomRight; break;
				default: selection = PalmSelection.Right; break;
			}
		}

		return selection;
	}

	private int getExtendedFingerCount(Frame frame) {
		FingerList fingerList = frame.Fingers.Extended();
		int count = 0;
		foreach( Finger finger in fingerList ) {
			if( finger.Type() != Leap.Finger.FingerType.TYPE_THUMB ) {
				count++;
			}
		}
		return count;
	}

	private void typeText(string key) {
		key = key.ToLower();
		char inputChar = (key == "space")
			? ' '
				: key.ToCharArray()[0];
		if (clickSound != null) { clickSound.Play(); }
		if (textReceiver != null) {
			textReceiver.receiveText(inputChar.ToString());
		} else {
			Debug.LogWarning("No text receiver specified!");
		}
	}
}
