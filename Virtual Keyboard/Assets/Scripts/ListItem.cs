using UnityEngine;
using System.Collections;

public abstract class ListItem : MonoBehaviour
{
	private bool isActive;
	public bool IsActive{
		get{
			return isActive;
		}
		set{
			isActive = value;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (isActive) {
			Debug.Log ("INTERACTING WITH LIST ITEM");
			execute ();
		}
	}
	
	public abstract void execute();
}
