using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudShooter : MonoBehaviour {

    public Transform Camera;

	void Start () {
		
	}
	
	void Update () {
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.position, Camera.forward, out raycastHit)) {
            GameObject hittedObject = raycastHit.collider.gameObject;
            if (hittedObject.CompareTag("cloud") && hittedObject.GetComponent<CloudBehaviour>().Floating) {
                hittedObject.GetComponent<CloudBehaviour>().HighLight();

                if (Input.GetMouseButtonDown(0))
                    hittedObject.GetComponent<CloudBehaviour>().Drop();
            }
        }
	}
}
