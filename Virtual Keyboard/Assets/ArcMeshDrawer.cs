using UnityEngine;
using System.Collections;

public class ArcMeshDrawer : MonoBehaviour {

	public float arcLength = (2*Mathf.PI);
	public float arcWeight = 1f;
	public Color startColor = Color.blue;
	public Color endColor = Color.blue;
	
	public Material arcMaterial;

	private Mesh mesh;
	private MeshFilter meshFilter;
	public int size = 20;

	private Vector3[] vertices;
	private int[] triangles;
	private Vector3[] normals;
	private Vector2[] uv;
	void Start () {
		//lineRenderer = new LineRenderer ();
		/*mesh = gameObject.GetComponent<Mesh> ();
		if (mesh == null) {
			mesh = gameObject.AddComponent<Mesh> ();
		}*/
		/*mesh = new Mesh ();
		meshFilter = gameObject.GetComponent<MeshFilter> ();
		Debug.Log (meshFilter);
		meshFilter.mesh = mesh;*/
		//meshFilter.s

		//size =  (int)Mathf.Round((((2 * Mathf.PI) / (float)numArcs) / arcLength));

		vertices = new Vector3[size+1];
		triangles = new int[3*(size-1)];
		
		normals = new Vector3[size + 1];
		uv = new Vector2[size + 1];
		
		for (int i = 0; i < uv.Length; i++)
			uv[i] = new Vector2(0, 0);
		for (int i = 0; i < normals.Length; i++)
			normals[i] = new Vector3(0, 1, 0);

		calculateMesh ();
		//triangleMesh ();
	}

	public void triangleMesh() {
		vertices = new Vector3[] {new Vector3(0, 0, 0), new Vector3(2f, 0, 0), new Vector3(2.8f, 1.0f, 0f)};
		uv = new Vector2[] {new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		triangles = new int[] {2, 1, 0};
	}

	public void calculateMesh () {
		float 	r = 3f,
		x = 1f,
		y = 0f,
		theta = 0f;
		int 	i = 0;
		Vector3 pos;

		float arcPointLength = (arcLength) / (size-1);

		while (i < (size-1)) {
			x = r*Mathf.Cos(theta);
			y = r*Mathf.Sin(theta);
			
			pos = new Vector3(x, y, 0);
			vertices[i] = pos;
			i++;
			theta += arcPointLength;
		}
		x = r*Mathf.Cos(arcLength);
		y = r*Mathf.Sin(arcLength);
		pos = new Vector3(x, y, 0);
		vertices [size-1] = pos;

		pos = new Vector3 (0, 0, 0);
		vertices [size] = pos;

		for (i = 0; i < size - 1; i++) {
			//Debug.Log ("Triangle: #" + i + " p1: " + vertices[size] + ", p2: " + vertices[i] + ", p3: " + vertices[i+1] );
			triangles[ (3*i) ] = i + 1;
			triangles[ (3*i) + 1 ] = i;
			triangles[ (3*i) + 2 ] = size;
		}
		Debug.Log ("Calculated Mesh" );
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log ("Updating Mesh");
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.triangles = triangles;
	}

	/*
	void Start()
	{
		mesh = new Mesh();
		mesh.vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
		mesh.triangles = new int[3 * 2 * quality];
		
		Vector3[] normals = new Vector3[4 * quality];
		Vector2[] uv = new Vector2[4 * quality];
		
		for (int i = 0; i < uv.Length; i++)
			uv[i] = new Vector2(0, 0);
		for (int i = 0; i < normals.Length; i++)
			normals[i] = new Vector3(0, 1, 0);
		
		mesh.uv = uv;
		mesh.normals = normals;
		
	}
	
	void Update()
	{
		float angle_lookat = 0;//GetEnemyAngle();
		
		float angle_start = angle_lookat - angle_fov;
		float angle_end = angle_lookat + angle_fov;
		float angle_delta = (angle_end - angle_start) / quality;
		
		float angle_curr = angle_start;
		float angle_next = angle_start + angle_delta;
		
		Vector3 pos_curr_min = Vector3.zero;
		Vector3 pos_curr_max = Vector3.zero;
		
		Vector3 pos_next_min = Vector3.zero;
		Vector3 pos_next_max = Vector3.zero;
		
		Vector3[] vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
		int[] triangles = new int[3 * 2 * quality];
		
		for (int i = 0; i < quality; i++)
		{
			Vector3 sphere_curr = new Vector3(
				Mathf.Sin(Mathf.Deg2Rad * (angle_curr)), 0,   // Left handed CW
				Mathf.Cos(Mathf.Deg2Rad * (angle_curr)));
			
			Vector3 sphere_next = new Vector3(
				Mathf.Sin(Mathf.Deg2Rad * (angle_next)), 0,
				Mathf.Cos(Mathf.Deg2Rad * (angle_next)));
			
			pos_curr_min = transform.position + sphere_curr * dist_min;
			pos_curr_max = transform.position + sphere_curr * dist_max;
			
			pos_next_min = transform.position + sphere_next * dist_min;
			pos_next_max = transform.position + sphere_next * dist_max;
			
			int a = 4 * i;
			int b = 4 * i + 1;
			int c = 4 * i + 2;
			int d = 4 * i + 3;
			
			vertices[a] = pos_curr_min; 
			vertices[b] = pos_curr_max;
			vertices[c] = pos_next_max;
			vertices[d] = pos_next_min;
			
			triangles[6 * i] = a;       // Triangle1: abc
			triangles[6 * i + 1] = b;  
			triangles[6 * i + 2] = c;
			triangles[6 * i + 3] = c;   // Triangle2: cda
			triangles[6 * i + 4] = d;
			triangles[6 * i + 5] = a;
			
			angle_curr += angle_delta;
			angle_next += angle_delta;
			
		}
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
	
		Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
	}*/
}