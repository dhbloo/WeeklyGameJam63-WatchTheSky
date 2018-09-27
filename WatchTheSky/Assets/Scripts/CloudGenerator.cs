using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

    public GameObject CloudObject;
    public int TargetCloudCount = 15;
    public float GenRadiusMin = 70f;
    public float GenRadiusMax = 120f;

    void Start () {
    }
	
	void Update () {
        int childCount = transform.childCount;
        if (childCount < TargetCloudCount) {
            float rndAngle = Random.value * Mathf.PI * 2;
            float rndRotateAngle = Random.value * 360;
            float rndRadius = Mathf.Lerp(GenRadiusMin, GenRadiusMax, Random.value);
            Vector3 spawnPos = new Vector3(Mathf.Cos(rndAngle) * rndRadius, 0, Mathf.Sin(rndAngle) * rndRadius);

            GameObject newCloud = Instantiate(CloudObject, transform);
            newCloud.transform.localPosition = spawnPos;
            newCloud.transform.localEulerAngles = new Vector3(0, rndRotateAngle, 0);
        }

    }
}
