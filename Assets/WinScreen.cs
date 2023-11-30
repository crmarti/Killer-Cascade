using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreen;

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
        winScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
