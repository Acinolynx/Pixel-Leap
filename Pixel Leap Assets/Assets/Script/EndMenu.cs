using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void OpenLevelSelect()
    {
        SceneManager.LoadScene("Level Select"); // Load the scene named "Level Select"
    }
    public void Quit()
    {
        Application.Quit();
    }
}
