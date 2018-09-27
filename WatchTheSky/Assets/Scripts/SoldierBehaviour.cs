using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    public float RisingSpeed;
    public float MoveSpeed;

    private bool canWalk;
    private GameObject Player;
    private Rigidbody rb;

    private bool isDeath;
    private float timer;
    private float timeDeath = 1.5f;


    private void Start()
    {
        canWalk = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        isDeath = false;
    }

    private void Update()
    {
        if(isDeath)
        {
            timer += Time.deltaTime;
            if (timer > timeDeath)
                Destroy(gameObject);
        }
        else if(canWalk)
        {
            Vector3 playerpos = Player.transform.position;
            playerpos.y = 0;
            Vector3 pos = transform.position;
            pos.y = 0;
            Vector3 forward = (playerpos - pos).normalized;
            transform.forward = -forward;
            float vy = rb.velocity.y;
            rb.velocity = forward * MoveSpeed + Vector3.up * vy;
        }
        else
        {
            rb.velocity = Vector3.up * RisingSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "terrain")
        {
            canWalk = true;
            rb.velocity = Vector3.up * 2f;
            foreach (BoxCollider boxColl in GetComponents<BoxCollider>())
                boxColl.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cloud")
        {
            foreach(BoxCollider boxColl in GetComponents<BoxCollider>())
                boxColl.enabled = false;
            isDeath = true;
            timer = 0;
        }
    }
}
