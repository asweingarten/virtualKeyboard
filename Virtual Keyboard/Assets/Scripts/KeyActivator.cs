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
	public bool setTextMeshManually = false;
	public float selectedColliderSizeModifier = 1.7f;

	private TextMesh textMesh;
	private Color baseColor;
	// Use this for initialization

	private SphereCollider collider;
	private float initialColliderSize;

	void Start ()
	{
		collider = GetComponent<SphereCollider>();
		if(collider != null ) initialColliderSize = collider.radius;
		Transform children = transform.Find ("Key_Text");
		if (children)
		{
			textMesh = children.GetComponent<TextMesh>();
			baseColor = textMesh.color;
			setTextMeshText(keyId);
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

	private void increaseCollider() {
		if( collider == null ) return;
		collider.radius = initialColliderSize*selectedColliderSizeModifier;
	}

	private void decreaseCollider() {
		if( collider == null ) return;
		collider.radius = initialColliderSize;
	}

	public void setActive(bool active) {
		if (active == true) {
			setColor(activeColor);
			increaseCollider();
		} else {
			setColor(baseColor);
			decreaseCollider();
		}
	}

	public void setColor(Color color) {
		textMesh.color = color;
	}

	private void setTextMeshText(string text) {
		if( textMesh != null && !setTextMeshManually ) {
			textMesh.text = text;
		}
	}

	/*void OnValidate() {
		TextMesh textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.text = keyId;
		gameObject.name = "Key_" + keyId;
	}*/
}

