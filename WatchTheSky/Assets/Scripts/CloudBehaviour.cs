using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public float MovingSpeedMin = 2.0f;
    public float MovingSpeedMax = 6.0f;

    float movingSpeed;

    void Start () {
        movingSpeed = Mathf.Lerp(MovingSpeedMin, MovingSpeedMax, Random.value);
    }
	
	void Update () {
        Transform t = transform;
        t.localPosition = t.localPosition + (-t.localPosition).normalized * movingSpeed * Time.deltaTime;
    }
}
