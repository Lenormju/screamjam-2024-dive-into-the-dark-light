using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    public void OpenCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void OpenMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
