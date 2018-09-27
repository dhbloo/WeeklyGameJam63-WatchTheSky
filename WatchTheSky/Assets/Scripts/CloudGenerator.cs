using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

    public Transform Player;
    public GameObject CloudObject;
    public int TargetCloudCount = 15;
    public float GenRadiusMin = 70f;
    public float GenRadiusMax = 120f;

    public float GenerateCycleTime = 1;

    private float timeGen = 0.1f;


    void Start () {
        for (int i = 0; i < 10; i++)
            GenerateCloud();
    }
	
	void Update () {
        if (timeGen == 0)
        {
            int childCount = transform.childCount;
            if (childCount < TargetCloudCount)
            {
                GenerateCloud();
            }
            timeGen += Time.deltaTime;
        }
        else
        {
            timeGen += Time.deltaTime;
            if (timeGen > GenerateCycleTime)
                timeGen = 0;
        }

    }

    void GenerateCloud()
    {
        float rndAngle = Random.value * Mathf.PI * 2;
        float rndRotateAngle = Random.value * 360;
        float rndRadius = Mathf.Lerp(GenRadiusMin, GenRadiusMax, Random.value);
        Vector3 spawnPos = new Vector3(Mathf.Cos(rndAngle) * rndRadius, 0, Mathf.Sin(rndAngle) * rndRadius);

        GameObject newCloud = Instantiate(CloudObject, transform);
        newCloud.transform.localPosition = spawnPos;
        newCloud.transform.localEulerAngles = new Vector3(0, rndRotateAngle, 0);

        newCloud.GetComponent<CloudBehaviour>().Player = Player;
    }
}
