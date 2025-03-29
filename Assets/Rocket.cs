using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rb;
    public float baseSpeed;
    public float accelerationFactor;
    public GameObject explosion;
    public AudioSource rocketeer;
 
    float timer;

    public float damage = 5f;
    public float timeToDie = 10f;

    public Vector3 scaleUp = new Vector3(40f, 40f, 40f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer += Time.fixedDeltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, scaleUp, 0.1f);
        

        rb.velocity = transform.forward * (baseSpeed * Mathf.Pow((1f + accelerationFactor), timer));


        if(timer > timeToDie)
        {
            Destroy(gameObject);
        }

       


        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject exp = Instantiate(explosion, transform);
        exp.transform.parent = null;
        exp.transform.position = transform.position;
        if (other.tag == "Ground")
        {
            if(rocketeer != null)
            {
                rocketeer = GameObject.FindGameObjectWithTag("ruckus").GetComponent<AudioSource>();

                rocketeer.Play();
            }
            Destroy(gameObject);
        }
        if(other.tag == "Enemy")
        {
            if (rocketeer != null)
            {
                rocketeer = GameObject.FindGameObjectWithTag("ruckus").GetComponent<AudioSource>();
                rocketeer.Play();
              
            }
            SpiderBossScript.bossHealth -= damage;
            Destroy(gameObject);

        }

        
    }
}
