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
    // Removed empty Update method
    // void Update()
    // {

    // }

    public void OpenScene(){
        // Use LevelLoader to load the scene with transition
        string sceneToLoad = "Lvl " + level.ToString();
        if (LevelLoader.instance != null)
        {
            LevelLoader.instance.LoadSceneByName(sceneToLoad);
        }
        else
        {
            Debug.LogError("LevelLoader instance not found!");
            // Fallback to direct loading if loader is missing
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
