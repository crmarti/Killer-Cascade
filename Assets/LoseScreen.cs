using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject LoseScreenUI;

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
        LoseScreenUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
