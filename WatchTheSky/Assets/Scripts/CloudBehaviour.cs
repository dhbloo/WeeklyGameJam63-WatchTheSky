using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour {

    public float MovingSpeedMin = 2.0f;
    public float MovingSpeedMax = 6.0f;

    public float Mass = 20.0f;
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

    void Start () {
        movingSpeed = Mathf.Lerp(MovingSpeedMin, MovingSpeedMax, Random.value);
        floating = true;
        lastHighlightTime = float.MinValue;
        isDeath = false;
    }
	
	void Update () {
        if (floating) {
            Transform t = transform;
            Vector3 dir = -t.localPosition;
            float dist = dir.magnitude * 0.01f;
            float dynamicScale = 2 * Mathf.Exp(dist) / (1 + Mathf.Exp(dist)) - 1;
            t.localPosition = t.localPosition + dir.normalized * (movingSpeed * dynamicScale * Time.deltaTime);
        }
         if(isDeath)
        {
            timer += Time.deltaTime;
            if (timer >= timeDeath)
                Destroy(gameObject);
            else
            {
                rb.velocity = Vector3.zero;
                gameObject.transform.localScale = Vector3.one * (timeDeath - timer) / timeDeath;
            }
        }
    }

    public void Drop() {
        floating = false;
        lastHighlightTime = float.MinValue;
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = Mass;
        rb.velocity = new Vector3(0, -InitialSpeed, 0);
        transform.localScale *= 2;
    }

    public void HighLight() {
        lastHighlightTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "terrain")
        {
            rb.velocity = Vector3.zero;
            isDeath = true;
            timer = 0;
        }
    }
}
