using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Added for UI components
using UnityEngine.SceneManagement; // Added for scene management

public class LevelSelector : MonoBehaviour
{
    public int level;
    public Text levelText; // Added to display the level number

    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level.ToString(); // Initialize the level text
    }

    // Update is called once per frame
    public void OpenScene(){
        SceneManager.LoadScene("Lvl " + level.ToString()); // Load the scene based on the level variable
    }
}
