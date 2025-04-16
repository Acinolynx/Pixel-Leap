using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Use LevelLoader to load the scene with transition
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadSceneByName("Lvl 1");
        }
        else
        {
            Debug.LogError("LevelLoader instance not found!");
            // Fallback to direct loading if loader is missing
            SceneManager.LoadScene("Lvl 1");
        }
    }

    // New function to load the Level Select scene
    public void OpenLevelSelect()
    {
        Debug.Log("StartMenu: OpenLevelSelect() called."); // Added log
        // Use LevelLoader to load the scene with transition
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadSceneByName("Level Select");
        }
        else
        {
            Debug.LogError("LevelLoader instance not found!");
            // Fallback to direct loading if loader is missing
            SceneManager.LoadScene("Level Select");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
