using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LowPolyTerrain : MonoBehaviour {

    public int Length;
    public int Width;

    public float Scale = 0.1f;
    public float HeightScale = 10.0f;
    public float TurbHeightScale = 0.1f;
    public float Offset = -4.0f;
    public float Turbulence = 0.8f;

    MeshFilter meshFilter;

    void Awake() {
        meshFilter = GetComponent<MeshFilter>();
        RebuildMesh();
    }

    void Start () {
        
	}

    void Update () {
		
	}

    public void RebuildMesh() {
        meshFilter.sharedMesh = CreateMesh();
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

    float[,] GenHeightField() {
        float[,] heights = new float[Length + 1, Width + 1];

        for (int x = 0; x <= Length; x++)
            for (int y = 0; y <= Width; y++) {
                float perlin = Mathf.PerlinNoise(x * Scale, y * Scale) * HeightScale;
                float random = (Random.value - 0.5f) * TurbHeightScale;
                heights[x, y] = Mathf.Max(perlin + Offset, 0) + random;
            }

        return heights;
    }

    float[,] GenTurbulenceField() {
        float[,] f = new float[Length + 1, Width + 1];

        for (int x = 0; x <= Length; x++)
            for (int y = 0; y <= Width; y++) {
                f[x, y] = (Random.value - 0.5f) * Turbulence;
            }

        for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++) {
                if (f[x, y] > 1 + f[x + 1, y])
                    f[x, y] = 1 + f[x + 1, y];
                if (f[x, y] > 1 + f[x, y + 1])
                    f[x, y] = 1 + f[x, y + 1];
            }

        return f;
    }

    void GenFlatTerrainVertices(out Vector3[] vertices, out int[] indices) {
        vertices = new Vector3[Length * Width * 6];
        indices = new int[Length * Width * 6];
        float x0 = -0.5f * Length, z0 = -0.5f * Width;
        int nv = 0;

        float[,] heightField = GenHeightField();
        float[,] xr = GenTurbulenceField();
        float[,] zr = GenTurbulenceField();

        for (int i = 0; i < Length; i++)
            for (int j = 0; j < Width; j++) {
                float x = i + x0, z = j + z0;
                vertices[nv++] = new Vector3(x + xr[i, j],              heightField[i, j],          z + zr[i, j]);
                vertices[nv++] = new Vector3(x + xr[i, j + 1],          heightField[i, j + 1],      z + 1 + zr[i, j + 1]);
                vertices[nv++] = new Vector3(x + 1 + xr[i + 1, j],      heightField[i + 1, j],      z + zr[i + 1, j]);
                vertices[nv++] = new Vector3(x + 1 + xr[i + 1, j],      heightField[i + 1, j],      z + zr[i + 1, j]);
                vertices[nv++] = new Vector3(x + xr[i, j + 1],          heightField[i, j + 1],      z + 1 + zr[i, j + 1]);
                vertices[nv++] = new Vector3(x + 1 + xr[i + 1, j + 1],  heightField[i + 1, j + 1],  z + 1 + zr[i + 1, j + 1]);
            }

        for (int i = 0; i < vertices.Length; i++)
            indices[i] = i;
    }

}
