using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PointCloud : MonoBehaviour
{
    public PhysicMaterial physicMaterial;

    private Mesh mesh;
    int numPoints = 60000;

    // Use this for initialization
    void Start()
    {
        mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = mesh;
        CreateMesh();
    }

    void CreateMesh()
    {
        Vector3[] points = new Vector3[numPoints];
        int[] indecies = new int[numPoints];
        Color[] colors = new Color[numPoints];
        for (int i = 0; i < points.Length; ++i)
        {
            points[i] = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            indecies[i] = i;
            colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
			if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();


            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
			if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indecies, MeshTopology.Points, 0);
        //meshRenderer.sharedMaterial = material;
		meshCollider.sharedMesh = mesh;
        //meshCollider.sharedMaterial = physicMaterial;
		
    }
}