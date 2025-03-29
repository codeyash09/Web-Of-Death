using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    public float speed;
    public float topSpeed;
    public float turnSpeed;
    public float gEarth = 9.8f;  // Gravity on the earth.
    Rigidbody rb;
    public Slider hp;

    public static float health;
    float hor;
    float vert;
    float topsHp;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100;
        topsHp = health;
    }

    public void Update()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    void FixedUpdate()
    {
        


        hp.value = health / topsHp;
        if (!SpiderBossScript.unableToMove)
        {
            if (Suspension.isGrounded)
            {
                rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * gEarth);

                if (rb.GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
                {
                    rb.GetComponent<Rigidbody>().velocity += transform.forward * (speed / 10f) * -vert;
                }
                else
                {
                    rb.GetComponent<Rigidbody>().velocity = transform.forward * speed * vert + (new Vector3(rb.velocity.x * 0.999f, rb.velocity.y, rb.velocity.z * 0.999f));
                }


                if (rb.velocity.magnitude > 1f)
                {
                    transform.eulerAngles += new Vector3(0, turnSpeed * hor * (rb.velocity.magnitude / (topSpeed / 2)) * vert, 0);
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
