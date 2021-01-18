using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

	public Material cubeMaterial;

	void CreateCircle()
	{
		Mesh mesh = new Mesh();
		mesh.name = "ScriptedMesh";

		Vector3[] vertices = new Vector3[6];
		Vector3[] normals = new Vector3[6];
		Vector2[] uvs = new Vector2[6];
		int[] triangles = new int[18];

		//all possible UVs. I just repeat these twice
		Vector2 uv1 = new Vector2(0f, 0f);
		Vector2 uv2 = new Vector2(1f, 0f);
		Vector2 uv3 = new Vector2(0f, 1f);
		Vector2 uv4 = new Vector2(0f, 0f);
		Vector2 uv5 = new Vector2(1f, 0f);
		Vector2 uv6 = new Vector2(0f, 1f);

		//all possible vertices
		Vector3 p0 = new Vector3(-0.5f, -0.5f, 0.5f);
		Vector3 p1 = new Vector3(0.5f, -0.5f, 0.5f);
		Vector3 p2 = new Vector3(0.5f, -0.5f, -0.5f);
		Vector3 p3 = new Vector3(-0.5f, -0.5f, -0.5f);
		Vector3 p4 = new Vector3(-0.5f, 0.5f, 0.5f);
		Vector3 p5 = new Vector3(0.5f, 0.5f, 0.5f);
		Vector3 p6 = new Vector3(0.5f, 0.5f, -0.5f);
		Vector3 p7 = new Vector3(-0.5f, 0.5f, -0.5f);


		vertices = new Vector3[] { p4, p5, p1, p4, p5, p1 }; //repeating these twice
		normals = new Vector3[] {Vector3.forward,
								 Vector3.forward,
								 Vector3.forward,
								 Vector3.forward,
								 Vector3.forward,
								 Vector3.forward
		};

		uvs = new Vector2[] { uv1, uv2, uv3, uv4, uv5, uv6 };
		triangles = new int[] { 3, 1, 0, 3, 2, 1, 3, 1, 0, 3, 2, 1, 3, 1, 0, 3, 2, 1 }; //repeating these thrice

		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		mesh.RecalculateBounds();

		GameObject circle = new GameObject("circle");
		circle.transform.parent = this.gameObject.transform;
		MeshFilter meshFilter = (MeshFilter)circle.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = mesh;
		MeshRenderer renderer = circle.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material = cubeMaterial;
	}

	// Use this for initialization
	void Start()
	{
		CreateCircle();
	}

	// Update is called once per frame
	void Update()
	{

	}
}

