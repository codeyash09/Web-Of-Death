using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public float timer = 0;

    private void Start()
    {
        timer = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer > 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
