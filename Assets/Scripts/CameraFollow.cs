using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    float smoothnessDamp = 0.01f;
  
    

   

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, car.position) > 2f)
        {
            
            transform.position = Vector3.Lerp(transform.position, car.position, smoothnessDamp * Vector3.Distance(transform.position, car.position));
            transform.LookAt(car);
            transform.GetChild(0).LookAt(car);
        }


    }
}
