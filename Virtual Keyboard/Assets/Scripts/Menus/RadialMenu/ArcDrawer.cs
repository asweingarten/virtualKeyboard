using UnityEngine;
using System.Collections;

public class ArcDrawer : MonoBehaviour {

	public float arcLength = (2*Mathf.PI);
	public float arcWeight = 1f;
	public Color startColor = Color.blue;
	public Color endColor = Color.blue;

	public Material arcMaterial;
	private float arcPointLength = 0.1f;
	private LineRenderer lineRenderer;
	public int size = 20;
	void Start () {
		//lineRenderer = new LineRenderer ();
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		if (lineRenderer == null) {
			lineRenderer = gameObject.AddComponent<LineRenderer> ();
		}

		//size =  (int)Mathf.Round((((2 * Mathf.PI) / (float)numArcs) / arcLength));
		arcPointLength = (arcLength) / (size-1);
		lineRenderer.material = new Material (arcMaterial);
		lineRenderer.SetColors (startColor, endColor);
		lineRenderer.SetWidth (0.1f, 0.1f);
		lineRenderer.SetVertexCount (size);
	}
	

	// Update is called once per frame
	void Update () {
		float 	r = 3f,
				x = 1f,
				y = 0f,
				theta = 0f;
		int 	i = 0;
		Vector3 pos;
		Vector3 transformedPosition;

		arcPointLength = (arcLength) / (size-1);
		while (i < (size-1)) {
			x = r*Mathf.Cos(theta);
			y = r*Mathf.Sin(theta);
			
			pos = new Vector3(x, y, 0);
			//Debug.Log (i);
			transformedPosition = transform.TransformVector(pos);
			lineRenderer.SetPosition(i, transformedPosition);
			i++;
			theta += arcPointLength;
		}
		x = r*Mathf.Cos(arcLength);
		y = r*Mathf.Sin(arcLength);
		pos = new Vector3(x, y, 0);
		transformedPosition = transform.TransformVector(pos);
		lineRenderer.SetPosition(size-1, transformedPosition);
	}
}
