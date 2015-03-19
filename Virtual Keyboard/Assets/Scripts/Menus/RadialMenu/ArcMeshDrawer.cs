﻿using UnityEngine;
using System.Collections;

public class ArcMeshDrawer : MonoBehaviour {

	public delegate void RadialMenuActionTrigger(GameObject selectedItem);
	public static event RadialMenuActionTrigger OnRadialMenuActionTrigger;

	public float arcWeight = 1f;
	public float radius = 3f;
	public float rimWidth = 0.4f;
	public Vector3 selectedScaleFactor = new Vector3(1.2f, 1.2f, 1.2f);

	[SerializeField]
	private string initialLabel = "Hello World";
	public string label {
		get {
			return initialLabel;
		}
		set {
			initialLabel = value;
			Transform textTransform = transform.FindChild ("ArcText");
			if (textTransform != null ) {
				arcText = textTransform.gameObject;
				TextMesh textMesh = arcText.GetComponent<TextMesh> ();
				textMesh.text = initialLabel;
			}
		}
	}
	private Vector3 originalScale;
	[SerializeField]
	private Material rimMaterial;
	public Material arcRimMaterial {
		get { 
			return rimMaterial;
		} 
		set {
			rimMaterial = value;
			if( arcRim == null ) {
				Transform arcRimTransform = transform.FindChild("ArcRim");
				if( arcRimTransform != null ) {
					arcRim = arcRimTransform.gameObject;
					MeshRenderer arcRimRenderer = arcRim.GetComponent<MeshRenderer> ();
					arcRimRenderer.material = arcRimMaterial;
				}
			} else {
				MeshRenderer arcRimRenderer = arcRim.GetComponent<MeshRenderer> ();
				arcRimRenderer.material = arcRimMaterial;
			}
		}
	}
	[SerializeField]
	private Material bodyMaterial;
	public Material arcBodyMaterial {
		get { 
			return bodyMaterial;
		} 
		set {
			bodyMaterial = value;
			if( arcBody == null ) {
				Transform arcBodyTransform = transform.FindChild("ArcBody");
				if( arcBodyTransform != null ) {
					arcBody = arcBodyTransform.gameObject;
					MeshRenderer arcBodyRenderer = arcBody.GetComponent<MeshRenderer> ();
					arcBodyRenderer.material = arcBodyMaterial;
				}
			} else {
				MeshRenderer arcBodyRenderer = arcBody.GetComponent<MeshRenderer> ();
				arcBodyRenderer.material = arcBodyMaterial;
			}
		}
	}

	private float privArcLength = (2*Mathf.PI);//Private member gaurded by arcLength Property
	public float arcLength {
		get {
			return privArcLength;
		}
		set {
			privArcLength = value;
			float textAngle = privArcLength/2 + (Mathf.Deg2Rad*gameObject.transform.rotation.eulerAngles).z;
			arcText.transform.localPosition = Vector3.zero;
			arcText.transform.rotation = gameObject.transform.rotation;
			if(textAngle > Mathf.PI/2 && textAngle < Mathf.PI*1.5 ){
				arcText.transform.localRotation = Quaternion.Euler( 0f, 0f, Mathf.Rad2Deg*((privArcLength / 2) + Mathf.PI));
				TextMesh textMesh = arcText.GetComponent<TextMesh>();
				textMesh.anchor = TextAnchor.MiddleRight;
				float lossyX = arcText.transform.lossyScale.x;
				arcText.transform.Translate(new Vector3 (-0.2f * lossyX, 0, 0));           
			} else {
				arcText.transform.localRotation = Quaternion.Euler( 0f, 0f, Mathf.Rad2Deg*(privArcLength / 2));
				TextMesh textMesh = arcText.GetComponent<TextMesh>();
				textMesh.anchor = TextAnchor.MiddleLeft;
				float lossyX = arcText.transform.lossyScale.x;
				arcText.transform.Translate( new Vector3 (0.3f*lossyX, 0, 0));
			}
			createMeshes();
		}
	}
	private string privSelectionObjectName = "index";
	public string selectionObjectName {
		get {
			return privSelectionObjectName;
		}
		set {
			privSelectionObjectName = value;
			updateChildComponentSelectionObjects();
		}
	}

	public int quality = 20;

	private GameObject arcBody;
	private GameObject arcRim;
	private GameObject arcText;
	private float arcPointLength;

	private Vector3[] sharedVertices;

	private Vector3[] rimVertices;
	private int[] rimTriangles;
	private Vector3[] rimNormals;
	private Vector2[] rimUv;

	private Vector3[] bodyVertices;
	private int[] bodyTriangles;
	private Vector3[] bodyNormals;
	private Vector2[] bodyUv;

	void Awake () {
		createComponentsIfNeeded ();
	}

	void Start () {
		createMeshes ();
	}

	private void updateChildComponentSelectionObjects() {
		if (arcBody == null) {
			Transform bodyTransform = transform.FindChild ("ArcBody");
			if( bodyTransform != null ) {
				arcBody = bodyTransform.gameObject;
			}
		}
		RadialMenuItemSelection itemSelection = arcBody.GetComponent<RadialMenuItemSelection> ();
		if (itemSelection) {
			itemSelection.selectionObjectName = privSelectionObjectName;
		}
		
		if (arcRim == null) {
			Transform rimTransform = transform.FindChild ("ArcBody");
			if( rimTransform != null ) {
				arcRim = rimTransform.gameObject;
			}
		}
		RadialActionSelection actionSelection = arcBody.GetComponent<RadialActionSelection> ();
		if (actionSelection) {
			actionSelection.selectionObjectName = privSelectionObjectName;
		}
	}

	private void addArcTextComponents() {
		arcText.name = "ArcText";
		arcText.transform.parent = gameObject.transform;
		arcText.transform.localPosition = Vector3.zero;
		arcText.transform.localScale = Vector3.one;
		arcText.transform.localRotation = Quaternion.identity;
		MeshRenderer textRenderer = arcText.AddComponent<MeshRenderer> ();
		TextMesh textMesh = arcText.AddComponent<TextMesh> ();
		textMesh.fontSize = 20;
		textMesh.characterSize = 0.2f;
		textMesh.anchor = TextAnchor.MiddleLeft;
		textMesh.text = label;
		textMesh.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
		textRenderer.material = textMesh.font.material;
	}

	private void addArcBodyComponents() {
		arcBody.name = "ArcBody";
		arcBody.transform.parent = gameObject.transform;
		arcBody.transform.localPosition = Vector3.zero;
		arcBody.transform.localScale = Vector3.one;
		arcBody.transform.localRotation = Quaternion.identity;
		arcBody.AddComponent<MeshFilter> ();
		arcBody.AddComponent<MeshCollider> ();
		MeshRenderer arcBodyRenderer = arcBody.AddComponent<MeshRenderer> ();
		RadialMenuItemSelection arcBodySelector = arcBody.AddComponent<RadialMenuItemSelection> ();
		arcBodySelector.selectionObjectName = selectionObjectName;
		arcBodyRenderer.material = arcBodyMaterial;
	}

	private void addArcRimComponents() {
		arcRim.name = "ArcRim";
		arcRim.transform.parent = gameObject.transform;
		arcRim.transform.localPosition = Vector3.zero;
		arcRim.transform.localScale = Vector3.one;
		arcRim.transform.localRotation = Quaternion.identity;
		arcRim.AddComponent<MeshFilter> ();
		arcRim.AddComponent<MeshCollider> ();
		MeshRenderer arcRimRenderer = arcRim.AddComponent<MeshRenderer> ();
		arcRimRenderer.material = arcRimMaterial;
	}

	public void removeComponents() {
		if (arcText == null) {
			Transform textTransform = transform.FindChild ("ArcText");
			if( textTransform != null ) {
				arcText = textTransform.gameObject;
				DestroyImmediate(arcText);
			} 
		} else {
			DestroyImmediate(arcText);
		}
		
		if (arcBody == null) {
			Transform bodyTransform = transform.FindChild ("ArcBody");
			if( bodyTransform != null ) {
				arcBody = bodyTransform.gameObject;
				DestroyImmediate(arcBody);
			}
		} else {
			DestroyImmediate(arcBody);
		}
		
		if (arcRim == null) {
			Transform rimTransform = transform.FindChild ("ArcRim");
			if( rimTransform != null ) {
				arcRim = rimTransform.gameObject;
				DestroyImmediate(arcRim);
			}
		} else {
			DestroyImmediate(arcRim);
		}
	}

	public void createComponentsIfNeeded() {
		if (arcText == null) {
			Transform textTransform = transform.FindChild ("ArcText");
			if( textTransform != null ) {
				arcText = textTransform.gameObject;
			} else {
				arcText = new GameObject();
				addArcTextComponents();
			}
		}
		
		if (arcBody == null) {
			Transform bodyTransform = transform.FindChild ("ArcBody");
			if( bodyTransform != null ) {
				arcBody = bodyTransform.gameObject;
			} else {
				arcBody = new GameObject();
				addArcBodyComponents();
			}
		}

		if (arcRim == null) {
			Transform rimTransform = transform.FindChild ("ArcRim");
			if( rimTransform != null ) {
				arcRim = rimTransform.gameObject;
			} else {
				arcRim = new GameObject();
				addArcRimComponents();
			}
		}
	}

	void OnDestroy() {
		RadialActionSelection.OnRadialActionSelected -= fireRadialAction;
	}

	private void fireRadialAction(GameObject selected) {
		if (OnRadialMenuActionTrigger != null) {
			OnRadialMenuActionTrigger (gameObject);
		}
		deselectSection ();
		transform.parent.gameObject.SetActive (false);
	}

	void OnDisable() {
		if (selected) {
			deselectSection();
		}
	}

	private bool selected = false;
	public void selectSection() {
		//Save current scale and calculate the selected scale based on scale factor
		originalScale = transform.localScale;
		Vector3 selectedScale = originalScale;
		selectedScale.Scale (selectedScaleFactor);
		transform.localScale = selectedScale;
		RadialActionSelection radialActionSelection = arcRim.AddComponent<RadialActionSelection> ();
		radialActionSelection.selectionObjectName = selectionObjectName;
		RadialActionSelection.OnRadialActionSelected += fireRadialAction;
		selected = true;
	}

	public void deselectSection() {
		transform.localScale = originalScale;

		//Remove delegate and destroy the RadialActionSelection component
		RadialActionSelection.OnRadialActionSelected -= fireRadialAction;
		Destroy (arcRim.GetComponent<RadialActionSelection> ());
		selected = false;
	}

	public void createMeshes() {
		calculateSizes ();
		computeSharedVertices ();
		createRimMesh ();
		createBodyMesh ();
		assignMeshes ();
	}

	private void calculateSizes() {	
		arcPointLength = (arcLength) / (quality-1);
		sharedVertices = new Vector3[quality];

		rimVertices = new Vector3[sharedVertices.Length * 2];
		rimTriangles = new int[2*3*sharedVertices.Length];
		rimNormals = new Vector3[sharedVertices.Length * 2];
		rimUv = new Vector2[sharedVertices.Length*2];

		bodyVertices = new Vector3[sharedVertices.Length + 1];
		bodyTriangles = new int[3*(sharedVertices.Length - 1)];
		bodyNormals = new Vector3[sharedVertices.Length + 1];
		bodyUv = new Vector2[sharedVertices.Length + 1];

		for (int i = 0; i < rimUv.Length; i++)
			rimUv[i] = new Vector2(0, 0);
		for (int i = 0; i < rimNormals.Length; i++)
			rimNormals[i] = new Vector3(0, 1, 0);

		for (int i = 0; i < bodyUv.Length; i++) {
			bodyUv[i] = new Vector2(0, 0);
		}
		for (int i = 0; i < bodyUv.Length; i++) {
			bodyNormals[i] = new Vector3(0, 1, 0);
		}
	}

	private Vector3[] createArc( float radius ) {
		Vector3[] arcPoints = new Vector3[sharedVertices.Length];
		float 	x = 1f,
				y = 0f,
				theta = 0f;
		int 	i = 0;
		Vector3 pos;
		while (i < (arcPoints.Length-1)) {
			x = radius*Mathf.Cos(theta);
			y = radius*Mathf.Sin(theta);
			
			pos = new Vector3(x, y, 0);
			arcPoints[i] = pos;
			i++;
			theta += arcPointLength;
		}
		x = radius*Mathf.Cos(arcLength);
		y = radius*Mathf.Sin(arcLength);
		pos = new Vector3(x, y, 0);
		arcPoints [sharedVertices.Length - 1] = pos;
		return arcPoints;
	}

	private void computeSharedVertices() {
		sharedVertices = createArc (radius - rimWidth);
	}

	public void createRimMesh () {
		sharedVertices.CopyTo ( rimVertices, 0 );
		Vector3[] outerRimVerticies = createArc (radius);
		outerRimVerticies.CopyTo (rimVertices, sharedVertices.Length);

		for (int i = 0; i < (sharedVertices.Length - 1); i++) {
			rimTriangles[(6*i)] = i;
			rimTriangles[(6*i) + 1] = i + 1;
			rimTriangles[(6*i) + 2] = sharedVertices.Length + i;

			rimTriangles[(6*i) + 3] = sharedVertices.Length + i;
			rimTriangles[(6*i) + 4] = i+1;
			rimTriangles[(6*i) + 5] = sharedVertices.Length + i + 1;
		}
	}

	public void createBodyMesh () {
		sharedVertices.CopyTo (bodyVertices, 0);

		Vector3 center = new Vector3 (0, 0, 0);
		bodyVertices [bodyVertices.Length - 1] = center;
		
		for (int i = 0; i < (bodyTriangles.Length/3); i++) {
			bodyTriangles[ (3*i) ] = i + 1;
			bodyTriangles[ (3*i) + 1 ] = i;
			bodyTriangles[ (3*i) + 2 ] = bodyVertices.Length-1;
		}
	}

	private void assignMeshes() {
		Mesh tempBodyMesh = arcBody.GetComponent<MeshFilter> ().mesh;
		Mesh bodyMesh = arcBody.GetComponent<MeshFilter> ().sharedMesh;
		if (bodyMesh == null) {
			bodyMesh = tempBodyMesh;
		}
		bodyMesh.Clear ();
		bodyMesh.vertices = bodyVertices;
		bodyMesh.normals = bodyNormals;
		bodyMesh.uv = bodyUv;
		bodyMesh.triangles = bodyTriangles;
		arcBody.GetComponent<MeshCollider> ().sharedMesh = bodyMesh;
		
		Mesh tempRimMesh = arcRim.GetComponent<MeshFilter>().mesh;
		Mesh rimMesh = arcRim.GetComponent<MeshFilter>().sharedMesh;
		if (rimMesh == null) {
			rimMesh = tempRimMesh;
		}
		rimMesh.Clear ();
		rimMesh.vertices = rimVertices;
		rimMesh.uv = rimUv;
		rimMesh.normals = rimNormals;
		rimMesh.triangles = rimTriangles;
		arcRim.GetComponent<MeshCollider> ().sharedMesh = rimMesh;
	}

	// Update is called once per frame
	void Update () {
		assignMeshes ();
	}

	void OnValidate() {
		Transform textTransform = transform.FindChild ("ArcText");

		if (textTransform != null ) {
			arcText = textTransform.gameObject;
			TextMesh textMesh = arcText.GetComponent<TextMesh> ();
			textMesh.text = label;
		}
	}
}