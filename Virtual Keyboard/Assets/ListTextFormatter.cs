using UnityEngine;
using System.Collections;

public class ListTextFormatter : MonoBehaviour {

	public Font font;
	public Color fontColor = Color.white;
	public TextAnchor textAnchor = TextAnchor.MiddleCenter;
	public TextAlignment textAlignment = TextAlignment.Center;

	void Awake () {
		if( font == null ) {
			font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void applyFormat(GameObject toFormat) {
		Debug.Log ("Apply Format");
		if (toFormat == null) return;
		TextMesh textMesh= toFormat.GetComponent<TextMesh>();
		MeshRenderer meshRenderer = toFormat.GetComponent<MeshRenderer>();
		if( textMesh == null || meshRenderer == null ) return;
		textMesh.color = fontColor;
		textMesh.font = font;
		meshRenderer.material = textMesh.font.material;
		textMesh.alignment = textAlignment;
		textMesh.anchor = textAnchor;
		toFormat.transform.localScale = new Vector3 (1, 10, 1);
		textMesh.fontSize = 20;
		textMesh.characterSize = 0.045f;
	}
}
