using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public Transform[] points;
    public Transform[] wheels;
    public float maxSuspensionHeight;
    public float suspensionForce;
    public static bool isGrounded =false;

    Vector3[] wheelPoints;

    Rigidbody rb;
    Vector3[] pts;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pts = new Vector3[points.Length];
        isGrounded = false;
        wheelPoints = new Vector3[wheels.Length];
    }


    void FixedUpdate()
    {
        
        
        for (int i = 0; i < pts.Length; i++) {
            pts[i] = points[i].transform.position;
        }

        int numberOfContacts = 0;


        for(int i = 0; i < pts.Length; i++){
            RaycastHit hit;

            if (Physics.Raycast(pts[i], -transform.up, out hit, maxSuspensionHeight))
            {
                numberOfContacts++;
                Debug.DrawRay(pts[i], -transform.up * hit.distance, Color.red);

                rb.AddForceAtPosition(transform.up * CalculatedForce(suspensionForce, hit.distance), pts[i]);
                wheelPoints[i] = hit.point + (transform.up * (1.75f / 2));
            }
            else
            {
                Debug.DrawRay(pts[i], -transform.up * maxSuspensionHeight, Color.white);
                wheelPoints[i] = (pts[i] + -transform.up * maxSuspensionHeight) + (transform.up * (1.75f / 2));

            }

            
            wheels[i].transform.position = wheelPoints[i];
        }

        if(numberOfContacts > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }

        foreach(Transform wheel in wheels)
        {
            wheel.transform.localEulerAngles += new Vector3(0, 0, -(rb.velocity.magnitude));
            
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
