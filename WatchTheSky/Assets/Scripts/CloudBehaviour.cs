using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public float MovingSpeedMin = 2.0f;
    public float MovingSpeedMax = 6.0f;

    public float Mass = 20.0f;

    float movingSpeed;
    bool floating;
    public bool Floating { get { return floating; } }

    void Start () {
        movingSpeed = Mathf.Lerp(MovingSpeedMin, MovingSpeedMax, Random.value);
        floating = true;
    }
	
	void Update () {
        if (floating) {
            Transform t = transform;
            t.localPosition = t.localPosition + (-t.localPosition).normalized * movingSpeed * Time.deltaTime;
        }
    }

    public void Drop() {
        floating = false;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = Mass;
    }
}
