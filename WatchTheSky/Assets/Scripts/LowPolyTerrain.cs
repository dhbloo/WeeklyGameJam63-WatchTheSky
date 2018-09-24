using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LowPolyTerrain : MonoBehaviour {

    public int Length;
    public int Width;

    MeshFilter meshFilter;

    void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = CreateMesh();
    }

    void Start () {
        
	}

    void Update () {
		
	}

    Mesh CreateMesh() {
        Vector3[] vertices;
        int[] indices;
        GenFlatTerrainVertices(out vertices, out indices);

        Mesh mesh = new Mesh();
        mesh.name = "Low Poly Terrain";
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

    void GenFlatTerrainVertices(out Vector3[] vertices, out int[] indices) {
        vertices = new Vector3[Length * Width * 6];
        indices = new int[Length * Width * 6];
        float x0 = -0.5f * Length, z0 = -0.5f * Width;
        int nv = 0;

        float[,] heights = new float[Length + 1, Width + 1];
        float[,] xr = new float[Length + 1, Width + 1];
        float[,] zr = new float[Length + 1, Width + 1];

        float scale = 0.1f;

        for (int x = 0; x <= Length; x++)
            for (int y = 0; y <= Width; y++) {
                heights[x, y] = Mathf.PerlinNoise(x * scale, y * scale) + (Random.value - 0.5f) * 0.1f;
                xr[x, y] = (Random.value - 0.5f) * 0.5f;
                zr[x, y] = (Random.value - 0.5f) * 0.5f;
            }

        for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++) {
                vertices[nv++] = new Vector3(x0 + x + xr[x, y], heights[x,y], z0 + y + zr[x, y]);
                vertices[nv++] = new Vector3(x0 + x + xr[x, y + 1], heights[x,y+1], z0 + y + 1 + zr[x, y + 1]);
                vertices[nv++] = new Vector3(x0 + x + 1 + xr[x + 1, y], heights[x+1,y], z0 + y + zr[x + 1, y]);
                vertices[nv++] = new Vector3(x0 + x + 1 + xr[x + 1, y], heights[x + 1, y], z0 + y + zr[x + 1, y]);
                vertices[nv++] = new Vector3(x0 + x + xr[x, y + 1], heights[x, y + 1], z0 + y + 1 + zr[x, y + 1]);
                vertices[nv++] = new Vector3(x0 + x + 1 + xr[x + 1, y + 1], heights[x+1, y+1], z0 + y + 1 + zr[x + 1, y + 1]);
            }

        for (int i = 0; i < vertices.Length; i++)
            indices[i] = i;
    }

}
