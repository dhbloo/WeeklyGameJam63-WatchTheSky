using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;

    public float speed = 1.0f;

    private void Update()
    {
        Vector3 right = camera.transform.right;
        Vector3 forward = Vector3.Cross(Vector3.up, right);
        transform.position += (-forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * speed;
    }
}
