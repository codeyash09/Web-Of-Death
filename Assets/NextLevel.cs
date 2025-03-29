using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public int levelAdd = 1;

    public void LoadLevel()
    {
        StartCoroutine(Leveler());
    }

    public IEnumerator Leveler()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelAdd);

    }
}
