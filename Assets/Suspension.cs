using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Transform[] points;
    public float maxSuspensionHeight;
    public float suspensionForce;


    Rigidbody rb;
    Vector3[] pts;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pts = new Vector3[points.Length];
    }


    void FixedUpdate()
    {



        for (int i = 0; i < pts.Length; i++) {
            pts[i] = points[i].transform.position;
        }




        foreach (Vector3 suspensionPoint in pts) {
            RaycastHit hit;

            if(Physics.Raycast(suspensionPoint, -transform.up, out hit, maxSuspensionHeight))
            {
                Debug.DrawRay(suspensionPoint, -transform.up * hit.distance, Color.red);

                rb.AddForceAtPosition(transform.up * CalculatedForce(suspensionForce, hit.distance), suspensionPoint);
            }
            else
            {
                Debug.DrawRay(suspensionPoint, -transform.up * maxSuspensionHeight, Color.white);
                
            }
        }
    }


    public float CalculatedForce(float susForce, float hitDistance)
    {
        float force;
        float dist = hitDistance;

        if(dist <= 0.01f)
        {
            dist = 0.01f;
        }

        force = (maxSuspensionHeight / dist) * susForce;


        return force;
    }
}
