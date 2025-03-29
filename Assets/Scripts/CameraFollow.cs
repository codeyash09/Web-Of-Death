using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;
    public Transform gunPos;
    Vector3 ogPos;
    public Transform cursor;
    float smoothnessDamp = 0.01f;
    public Transform fakeCam;


    private void Start()
    {
        ogPos = transform.GetChild(0).localPosition;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, car.position) > 0f)
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, ogPos, smoothnessDamp);
            transform.position = Vector3.Lerp(transform.position, car.position, smoothnessDamp * Vector3.Distance(transform.position, car.position));
            transform.LookAt(car);
            transform.GetChild(0).LookAt(car);
        }
        

        



    }
}
