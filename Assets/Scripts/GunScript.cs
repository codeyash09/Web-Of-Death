using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform cursor;
    Camera mainCamera;

    bool reachedClosed;
    bool firing;

    public AudioSource mgBlast;
    public AudioSource rocketLaunch;

    float vfxthreshold = 0.05f;

    public GameObject roboRocket;
    public GameObject roboBullet;
    public float weapon;
    public GameObject mGbarrel;
    public GameObject rLcannon;

    public float maxRelatTimer;
    float reladTimer;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        reachedClosed = false;
        firing = false;
        weapon = 2;
        reladTimer = maxRelatTimer;
    }


    


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !firing)
        {
            firing = true;
            
        }

        if (firing)
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            weapon = 1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            weapon = 2;
        }



        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 90f, 0);

        transform.GetChild(0).GetChild(2).localRotation = Quaternion.Euler(0, 0, Camera.main.transform.eulerAngles.x);


    }

    public void Shoot()
    {
        //Effect Should be Before Attack
        //open -5 closed -3.7
        if (weapon == 1)
        {
            rLcannon.SetActive(true);
            mGbarrel.SetActive(false);
            Transform barrel = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(1).transform;
            if (!reachedClosed)
            {


                barrel.localPosition = new Vector3(Mathf.Lerp(barrel.localPosition.x, -3.7f, 0.1f), barrel.localPosition.y, barrel.localPosition.z);


                if (Mathf.Abs(barrel.localPosition.x - (-3.7f)) < vfxthreshold)
                {
                    barrel.localPosition = new Vector3(-3.7f, barrel.localPosition.y, barrel.localPosition.z);
                    reachedClosed = true;
                }
            }


            if (reachedClosed)
            {


                barrel.localPosition = new Vector3(Mathf.Lerp(barrel.localPosition.x, -5f, 0.25f), barrel.localPosition.y, barrel.localPosition.z);


                if (Mathf.Abs(barrel.localPosition.x - (-5f)) < vfxthreshold)
                {
                    barrel.localPosition = new Vector3(-5f, barrel.localPosition.y, barrel.localPosition.z);
                    reachedClosed = false;




                    Camera.main.GetComponent<CameraShaker>().shakeDuration = 0.05f;

                    //Add Attack Stuff Here
                    rocketLaunch.Play();
                    GameObject rocket = Instantiate(roboRocket, transform.GetChild(0).GetChild(2).transform);

                    rocket.transform.parent = null;


                    //In Front of Here

                    firing = false;
                }
            }
        }
        else
        {
            rLcannon.SetActive(false);
            mGbarrel.SetActive(true);
            mGbarrel.transform.Rotate(0, 0, 5f);

            if(reladTimer <= 0)
            {
                GameObject bullet = Instantiate(roboBullet, transform.GetChild(0).GetChild(2).transform);
                bullet.transform.localEulerAngles += new Vector3(0, -90f, 0);
                bullet.transform.parent = null;

                reladTimer = maxRelatTimer;
                mgBlast.Play();
                firing = false;
                Camera.main.GetComponent<CameraShaker>().shakeDuration = 0.01f;
                Camera.main.GetComponent<CameraShaker>().shakeAmount = 0.05f;


            }
            else
            {
                reladTimer -= Time.fixedDeltaTime;
            }
        }



    }
}
