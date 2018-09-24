using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;

    public float speed = 1.0f;

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 right = camera.transform.right;
        Vector3 forward = Vector3.Cross(Vector3.up, right);
        float vy = rb.velocity.y;
        rb.velocity = (-forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed + vy * Vector3.up;
    }
}
