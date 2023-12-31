using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    public Button loadGameButton;
    public Button newGameButton;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {   
        if (!DataPersistenceManager.instance.HasGameData()) {
            loadGameButton.interactable = false;
        }
        else
        {
            loadGameButton.interactable = true;
        }
    }

    public void PlayGame()
    {
        DisableAllButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        DisableAllButtons();
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.getSaveData().levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableAllButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }
}
