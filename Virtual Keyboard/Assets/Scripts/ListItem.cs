using UnityEngine;
using System.Collections;

public abstract class ListItem : MonoBehaviour
{	
	[SerializeField]
	private string displayText = "";
	public string itemText {
		get {
			return displayText;
		}
		set {
			displayText = value;
			updateTextMesh();
		}
	}
	
	public abstract void onItemChosen();

	private void updateTextMesh () {
		TextMesh textMesh = GetComponentInChildren<TextMesh> ();
		if( textMesh != null ) {
			textMesh.text = displayText;
		}
	}

	void OnValidate() {
		updateTextMesh ();
	}
}
