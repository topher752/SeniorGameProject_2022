using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart_Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        // Turn this on for the build game
        Cursor.visible = true;
        Application.Quit();

        // This is just for the editor
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
