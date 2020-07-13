using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]

public class ProceduralMesh : MonoBehaviour
{
	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;

	void Awake()
	{
		mesh = GetComponent<MeshFilter>().mesh;
	}

    // Start is called before the first frame update
    void Start()
    {
        MakeMeshData();
        CreateMesh();
    }

    // create array of vertices & array of integers
    void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(0.1f, 0f, 0.2f), new Vector3(0.2f, 0f, 0f) };

        triangles = new int[] {0, 1, 2};
    }

    void CreateMesh()
    {
    	mesh.Clear();
    	mesh.vertices = vertices;
    	mesh.triangles = triangles;
    }
}
