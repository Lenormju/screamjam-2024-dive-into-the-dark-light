using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("integration_pinjoni-3");
    }

    public void StartIntro()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
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
