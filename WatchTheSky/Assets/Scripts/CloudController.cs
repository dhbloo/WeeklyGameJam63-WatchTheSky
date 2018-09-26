using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    public GameObject CloudObject;
    public int TargetCloudCount = 15;
    public float GenRadius = 100f;
    public float MovingSpeed = 2.0f;

	void Start () {
    }
	
	void Update () {
        int childCount = transform.childCount;
        if (childCount < TargetCloudCount) {
            float rndAngle = Random.value * Mathf.PI * 2;
            float rndRotateAngle = Random.value * 360;
            Vector3 spawnPos = new Vector3(Mathf.Cos(rndAngle) * GenRadius, 0, Mathf.Sin(rndAngle) * GenRadius);

            GameObject newCloud = Instantiate(CloudObject, transform);
            newCloud.transform.localPosition = spawnPos;
            newCloud.transform.localEulerAngles = new Vector3(0, rndRotateAngle, 0);
        }

        for (int i = 0; i < childCount; i++) {
            Transform t = transform.GetChild(i);
            t.localPosition = t.localPosition + (-t.localPosition).normalized * MovingSpeed * Time.deltaTime;
        }

    }
}
