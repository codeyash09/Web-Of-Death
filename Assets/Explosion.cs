using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
