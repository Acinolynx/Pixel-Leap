using UnityEngine;
using UnityEngine.UI; // Required if GameManager updates UI directly, otherwise optional

public class GameManager : MonoBehaviour
{
    // Singleton instance pattern
    public static GameManager instance;

    // Variable to store the total cherries collected across scenes
    public int totalCherries = 0;

    // Optional: Reference to the cherry text UI if you want GameManager to update it
    // public Text cherryTextUI; // Assign this in the Inspector if needed

    private void Awake()
    {
        // Implement the Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
            Debug.Log("GameManager Instance Created. Initial Cherries: " + totalCherries);
        }
        else if (instance != this) // If another instance already exists
        {
            Debug.LogWarning("Duplicate GameManager detected. Destroying this one.");
            Destroy(gameObject); // Destroy the duplicate
        }
    }

    // Public method to add cherries (good practice)
    public void AddCherries(int amount)
    {
        totalCherries += amount;
        Debug.Log("Cherries Added. Total Cherries: " + totalCherries);
        // Optional: Update UI directly from here if you have the reference
        // UpdateCherryUI();
    }

    // Public method to get the current count (good practice)
    public int GetCherryCount()
    {
        return totalCherries;
    }

    // Optional: Method to update UI if GameManager holds the reference
    // public void UpdateCherryUI()
    // {
    //     if (cherryTextUI != null)
    //     {
    //         cherryTextUI.text = "Cherries: " + totalCherries;
    //     }
    //     else
    //     {
    //         Debug.LogWarning("GameManager: CherryTextUI reference not set in Inspector.");
    //     }
    // }

    // Optional: Call this from Start() in ItemCollector if needed
    // void Start()
    // {
    //     UpdateCherryUI(); // Update UI when the game starts or scene loads
    // }
}
