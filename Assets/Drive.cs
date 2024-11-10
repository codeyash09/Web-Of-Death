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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * gEarth);

        if (rb.GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
        {
            rb.GetComponent<Rigidbody>().velocity += transform.forward * (speed / 10f) * -Input.GetAxis("Vertical");
        }
        else
        {
            rb.GetComponent<Rigidbody>().velocity = transform.forward * speed * Input.GetAxis("Vertical") +
                          (new Vector3(rb.velocity.x * 0.999f, rb.velocity.y, rb.velocity.z * 0.999f));
        }

        
        if (rb.velocity.magnitude > 0.5f) 
        {
            transform.eulerAngles += new Vector3(0, turnSpeed * Input.GetAxis("Horizontal"), 0);
        }
    }
}
