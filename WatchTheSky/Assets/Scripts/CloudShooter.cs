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
            if (hittedObject.tag == "cloud" && hittedObject.GetComponent<Rigidbody>() == null) {
                // TODO : 加上高亮
                hittedObject.transform.localScale = new Vector3(2, 2, 2);

                if (Input.GetMouseButtonDown(0)) {
                    hittedObject.AddComponent<Rigidbody>();
                }
            }
        }
	}
}
