using UnityEngine;
using System.Collections;

public class ArcMeshDrawer : MonoBehaviour {

	public float arcLength = (2*Mathf.PI);
	public float arcWeight = 1f;
	public float radius = 3f;
	public float rimWidth = 0.4f;
	;

	public Material arcRimMaterial;
	public Material arcBodyMaterial;
	
	public int quality = 20;

	private GameObject arcBody;
	private GameObject arcRim;
	private float arcPointLength

	private Vector3[] sharedVertices;

	private Vector3[] rimVertices;
	private int[] rimTriangles;
	private Vector3[] rimNormals;
	private Vector2[] rimUv;

	private Vector3[] bodyVertices;
	private int[] bodyTriangles;
	private Vector3[] bodyNormals;
	private Vector2[] bodyUv;

	void Start () {
		arcBody = new GameObject ();
		arcBody.name = "ArcBody";
		arcBody.transform.parent = gameObject.transform;
		arcBody.AddComponent<MeshFilter> ();
		MeshRenderer arcBodyRenderer = arcBody.AddComponent<MeshRenderer> ();
		arcBodyRenderer.material = arcBodyMaterial;

		arcRim = new GameObject ();
		arcRim.name = "ArcRim";
		arcRim.transform.parent = gameObject.transform;
		arcRim.AddComponent<MeshFilter> ();
		MeshRenderer arcRimRenderer = arcRim.AddComponent<MeshRenderer> ();
		arcRimRenderer.material = arcRimMaterial;

		createMeshes ();
	}

	public void createMeshes() {
		calculateSizes ();
		computeSharedVerticies ();
		createRimMesh ();
		createBodyMesh ();
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
	private void computeSharedVerticies() {
		sharedVertices = createArc (radius - rimWidth);
		Debug.Log (sharedVertices);
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

	// Update is called once per frame
	void Update () {
		Mesh bodyMesh = arcBody.GetComponent<MeshFilter> ().mesh;
		bodyMesh.Clear ();
		bodyMesh.vertices = bodyVertices;
		bodyMesh.normals = bodyNormals;
		bodyMesh.uv = bodyUv;
		bodyMesh.triangles = bodyTriangles;

		Mesh rimMesh = arcRim.GetComponent<MeshFilter>().mesh;
		rimMesh.Clear ();
		rimMesh.vertices = rimVertices;
		rimMesh.uv = rimUv;
		rimMesh.normals = rimNormals;
		rimMesh.triangles = rimTriangles;
	}
}