using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl 1");
    }

    // New function to load the Level Select scene
    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("Level Select"); // Load the scene named "Level Select"
    }

    public void Quit()
    {
        Application.Quit();
    }
}
