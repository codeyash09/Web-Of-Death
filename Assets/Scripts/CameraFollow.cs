using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    float smoothnessDamp = 0.1f;
  
    

   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, car.position) > 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, car.position, smoothnessDamp);
            transform.LookAt(car);
            transform.GetChild(0).LookAt(car);
        }


    }
}
