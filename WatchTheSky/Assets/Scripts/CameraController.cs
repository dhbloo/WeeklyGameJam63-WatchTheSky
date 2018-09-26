using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float maxElevationAngle = 80f;
    public float minElevationAngle = -80f;

    public float eyeOffset = 0.6f;
    

    private void Update()
    {
        transform.position = player.transform.position + Vector3.up * eyeOffset;

        float rotationAngleX = Input.GetAxis("Mouse X");
        float rotationAngleY = Input.GetAxis("Mouse Y");

        float nowElevationAngle = 90f - Vector3.SignedAngle(Vector3.up, transform.forward, transform.right);

        float MaxRotationAngleY = maxElevationAngle - nowElevationAngle;
        float MinRotationAngleY = minElevationAngle - nowElevationAngle;

        rotationAngleY = Mathf.Clamp(rotationAngleY, MinRotationAngleY, MaxRotationAngleY);

        transform.Rotate(Vector3.up * rotationAngleX - transform.right * rotationAngleY, Space.World);

    }

}

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GUILayout.TextArea("Info : Open 'Edit->Project Settings->Input' to adjust sensitivity of MouseX and MouseY");
    }
}