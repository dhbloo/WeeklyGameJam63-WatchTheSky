using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPolyTerrain : MonoBehaviour {

    private Mesh mesh;
	
	void Start () {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;
	}
	
	void Update () {
		
	}

    void GenerateMesh() {
        
    }

}
