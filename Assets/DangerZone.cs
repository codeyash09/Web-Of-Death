using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float timer;
    public bool kill;

    private void Start()
    {
        kill = false;
        timer = 0;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer >= 5f)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
            kill = true;
            if(timer >= 5.2f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 10000, transform.position.z), 0.01f);
                if(timer >= 9f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (kill && other.tag == "Player")
        {
            Drive.health -= 30f;

            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (kill && other.tag == "Player")
        {
            Drive.health -= 30f;



        }
    }
}
