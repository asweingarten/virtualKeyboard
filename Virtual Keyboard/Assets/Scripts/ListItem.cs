using UnityEngine;
using System.Collections;

public abstract class ListItem : MonoBehaviour
{
	public bool IsActive;
	[SerializeField]
	protected string displayText = "";
	/*public bool IsActive{
		get{
			return isActive;
		}
		set{
			isActive = value;
		}
	}*/

	void OnCollisionEnter(Collision collision) {
		if (IsActive) {
			Debug.Log ("INTERACTING WITH LIST ITEM");	
			execute ();
		}
	}
	
	public abstract void execute();

	void OnValidate() {
		TextMesh textMesh = GetComponentInChildren<TextMesh> ();
		if( textMesh != null ) {
			textMesh.text = displayText;
		}
	}
}
