using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void GameWin()
    {
        SceneManager.LoadScene("GameWin");
    }

    public void GameLoss()
    {
        SceneManager.LoadScene("GameLoss");
    }

    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/ElMario90/Unity_Project_Final_Version");
    }
}
