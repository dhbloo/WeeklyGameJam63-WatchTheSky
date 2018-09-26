using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float sensitivityLeftRight = 1.0f;
    public float sensitivityUpDown = 1.0f;

    public float maxElevationAngle = 80.0f;

    public float eyeOffset = 0.6f;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = player.transform.position + Vector3.up * eyeOffset;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivityLeftRight -
            transform.right * Input.GetAxis("Mouse Y") * sensitivityUpDown, Space.World);
        
    }

}
