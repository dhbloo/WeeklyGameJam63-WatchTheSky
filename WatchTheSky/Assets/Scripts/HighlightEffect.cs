using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HighlightEffect : MonoBehaviour {

    public Material DrawMaterial;
    public Material HighlightMaterial;
    public Material GaussianMaterial;

    public GameObject CloudGroup;

    RenderTexture highlightRT;
    RenderTargetIdentifier rtID;

    CommandBuffer renderBuffer;
    List<GameObject> highlightObjects;

    void Start () {
        CreateBuffers();
    }
	
	void Update () {
		
	}

    void CreateBuffers() {
        highlightRT = new RenderTexture(Screen.width, Screen.height, 0);
        rtID = new RenderTargetIdentifier(highlightRT);

        renderBuffer = new CommandBuffer();
    }

    void RenderHighlights() {
        renderBuffer.SetRenderTarget(rtID);

        renderBuffer.ClearRenderTarget(true, true, new Color(0, 0, 0, 0));
        for (int i = 0; i < CloudGroup.transform.childCount; i++) {
            Transform cloud = CloudGroup.transform.GetChild(i);
            if (cloud.gameObject.GetComponent<CloudBehaviour>().Highlighting) {
                Renderer renderer = cloud.gameObject.GetComponent<Renderer>();
                renderBuffer.DrawRenderer(renderer, DrawMaterial, 0);
            }
        }

        RenderTexture.active = highlightRT;
        Graphics.ExecuteCommandBuffer(renderBuffer);
        RenderTexture.active = null;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        renderBuffer.Clear();
        RenderHighlights();

        RenderTexture rt = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        Graphics.Blit(highlightRT, rt, GaussianMaterial);

        // Excluding the original image from the blurred image, leaving out the areal alone
        //HighlightMaterial.SetTexture("_OccludeMap", highlightRT);
        //Graphics.Blit(rt, rt, HighlightMaterial, 0);
        // Just combining two textures together
        HighlightMaterial.SetTexture("_OccludeMap", rt);
        Graphics.Blit(source, destination, HighlightMaterial, 1);

        RenderTexture.ReleaseTemporary(rt);
    }
}
