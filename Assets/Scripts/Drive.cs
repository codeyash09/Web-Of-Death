using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed;
    public float topSpeed;
    public float turnSpeed;
    public float gEarth = 9.8f;  // Gravity on the earth.
    Rigidbody rb;

    float hor;
    float vert;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public void Update()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
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
                transform.eulerAngles += new Vector3(0, turnSpeed * hor * (rb.velocity.magnitude / (topSpeed/2)) * vert, 0);
            }
        }
    }
}
