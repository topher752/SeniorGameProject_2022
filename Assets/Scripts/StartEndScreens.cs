using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartEndScreens : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject settingsPanel;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        controlsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        // Turn this on for the build game
        Application.Quit();

        // This is just for the editor
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ViewControls()
    {
        controlsPanel.SetActive(true);
    }

    public void BackToGame()
    {
        controlsPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ViewSettings()
    {
        settingsPanel.SetActive(true);
    }
}
