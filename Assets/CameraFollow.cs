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
        transform.position = Vector3.Lerp(transform.position, car.position, smoothnessDamp);
        transform.LookAt(car);
        transform.GetChild(0).LookAt(car);
    }
}
