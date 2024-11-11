using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform cursor;
    Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            cursor.position = hit.point;
        }
    }


    void FixedUpdate()
    {
        Vector3 direction = cursor.position - transform.position;

        

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 90f, 0);


            transform.GetChild(0).transform.GetChild(2).transform.localRotation = Quaternion.Euler(0, 0, rotation.eulerAngles.x);
            if(transform.GetChild(0).GetChild(2).transform.localRotation.eulerAngles.z > 30f)
            {
                transform.GetChild(0).GetChild(2).transform.localRotation = Quaternion.Euler(0, 0, 30f);
            }
        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.GetChild(0).GetChild(2).GetChild(0).transform.Rotate(0, 0, 15f);
        }
        
        
    }
}
