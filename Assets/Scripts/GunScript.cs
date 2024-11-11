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


    private void FixedUpdate()
    {
        
    }
}
