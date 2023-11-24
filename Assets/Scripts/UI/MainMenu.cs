using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button loadGameButton;

    private void Start()
    {
        loadGameButton.interactable = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadPlayer();

        SceneManager.LoadScene(data.getLevelIndex());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
