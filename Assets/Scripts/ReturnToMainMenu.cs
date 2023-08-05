using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public void OnFinishButtonClick()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Game");
    }
}
