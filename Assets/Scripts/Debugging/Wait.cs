using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait : MonoBehaviour
{
    public float waitTime = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScreenWait());
    }

    IEnumerator ScreenWait()
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
