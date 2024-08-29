using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public GameObject PauseScreen;
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PausePressed()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartPressed()
    {
        PauseScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumePressed()
    {
        Time.timeScale = 1.0f;
        PauseScreen.SetActive(false);
    }
}