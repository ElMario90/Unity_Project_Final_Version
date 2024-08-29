using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoss : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void RestartPressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
