using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudShooter : MonoBehaviour {

    public Transform Camera;
    public GameObject LightBar;

	void Start () {
		
	}
	
	void Update () {
        RaycastHit raycastHit;
        LightBar.SetActive(false);
        if (Physics.Raycast(Camera.position, Camera.forward, out raycastHit)) {
            GameObject hittedObject = raycastHit.collider.gameObject;
            if (hittedObject.CompareTag("cloud") && hittedObject.GetComponent<CloudBehaviour>().Floating) {
                hittedObject.GetComponent<CloudBehaviour>().HighLight();

                LightBar.SetActive(true);
                LightBar.transform.position = hittedObject.transform.position;

                if (Input.GetMouseButtonDown(0))
                    hittedObject.GetComponent<CloudBehaviour>().Drop();
            }
        }
	}
}
