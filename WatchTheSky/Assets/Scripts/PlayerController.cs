using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject CameraObject;
    public float PlayerSpeed = 1.0f;

    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 right = CameraObject.transform.right;
        Vector3 forward = Vector3.Cross(Vector3.up, right) * Input.GetAxis("Vertical");
        right = right * Input.GetAxis("Horizontal");
        float vy = rb.velocity.y;
        rb.velocity = (-forward + right) * PlayerSpeed + vy * Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "soldier" || collision.gameObject.tag == "cloud")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().EndGame();
    }

}
