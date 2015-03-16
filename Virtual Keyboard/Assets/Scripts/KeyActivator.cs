using UnityEngine;
using System.Collections;

public class KeyActivator : MonoBehaviour
{
	public delegate void KeyLeapFocusAction(KeyActivator keyId);
	public delegate void KeyLeapFocusLostAction(KeyActivator keyId);
	public static event KeyLeapFocusAction OnKeyLeapFocus;
	public static event KeyLeapFocusLostAction OnKeyLeapFocusLost;

	public Color activeColor;
	public string keyId;

	private TextMesh textMesh;
	private Color baseColor;
	// Use this for initialization
	void Start ()
	{
		Transform children = transform.Find ("Key_Text");
		if (children)
		{
			textMesh = children.GetComponent<TextMesh>();
			baseColor = textMesh.color;
			textMesh.text = keyId;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		/*if (Input.GetKeyDown (keyId)) {
			OnKeyLeapPressed (keyId);
			setColor(activeColor);
		} else if (Input.GetKeyUp (keyId)) 
		{
			OnKeyLeapReleased (keyId);
			setColor(baseColor);
		}*/
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log ("Something hit the " + keyId + " key");
		if (collision.gameObject.name == "Plane")
			return;
		OnKeyLeapFocus(this);
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.name == "Plane")
			return;
		OnKeyLeapFocusLost(this);
	}

	public void setActive(bool active) {
		if (active == true) {
			setColor(activeColor);
		} else {
			setColor(baseColor);
		}
	}

	public void setColor(Color color) {
		textMesh.color = color;
	}

	/*void OnValidate() {
		TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.text = keyId;
		gameObject.name = "Key_" + keyId;
	}*/
}

