using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public float MovingSpeedMin = 2.0f;
    public float MovingSpeedMax = 6.0f;

    public float InitialSpeed = 4;

    Rigidbody rb;

    float movingSpeed;
    bool floating;
    public bool Floating { get { return floating; } }
    float lastHighlightTime;
    public bool Highlighting { get { return Time.time - lastHighlightTime <= 0.1; } }

    // timer for death
    bool isDeath;
    float timer;
    public float timeDeath = 3f;
    Vector3 initialScale;

    void Start () {
        movingSpeed = Mathf.Lerp(MovingSpeedMin, MovingSpeedMax, Random.value);
        floating = true;
        lastHighlightTime = float.MinValue;
        isDeath = false;
        
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if (floating) {
            Transform t = transform;
            Vector3 dir = -t.localPosition;
            float dist = dir.magnitude * 0.01f;
            float dynamicScale = 2 * Mathf.Exp(dist) / (1 + Mathf.Exp(dist)) - 1;
            t.localPosition = t.localPosition + dir.normalized * (movingSpeed * dynamicScale * Time.deltaTime);

        }

        if (isDeath) {
            timer += Time.deltaTime;
            if (timer >= timeDeath)
                Destroy(gameObject);
            else {
                rb.velocity = Vector3.zero;
                gameObject.transform.localScale = initialScale * (timeDeath - timer) / timeDeath;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (!gameObject.activeSelf)
            return;

        GameObject obj = other.gameObject;
        if (obj.CompareTag("cloud")) {
            if (obj.GetComponent<CloudBehaviour>().Floating && 
                obj.transform.localScale.magnitude <= transform.localScale.magnitude) {
                transform.localScale += obj.transform.localScale * 0.2f;
                obj.SetActive(false);
                Destroy(obj);
            }
        }
    }

    public void Drop() {
        floating = false;
        lastHighlightTime = float.MinValue;
        rb.isKinematic = false;
        rb.velocity = new Vector3(0, -InitialSpeed, 0);
        GetComponent<MeshCollider>().isTrigger = false;
        transform.localScale *= 1.1f;
    }

    public void HighLight() {
        lastHighlightTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "terrain"
            && this)
        {
            rb.velocity *= 0.2f;
            isDeath = true;
            timer = 0;
        }
    }
}
