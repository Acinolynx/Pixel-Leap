using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // Removed local cherry count: private int cherries = 0;

    [SerializeField] private Text cherriesText; // Keep reference to UI Text
    [SerializeField] private AudioSource collectSoundEffect;

    // Add Start method to initialize text from GameManager
    private void Start()
    {
        UpdateCherryText(); // Update text when the scene/object starts
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            // Play sound only if the reference exists
            if (collectSoundEffect != null)
            {
                collectSoundEffect.Play();
            }
            else
            {
                 Debug.LogWarning("ItemCollector: CollectSoundEffect not assigned in Inspector.");
            }

            Destroy(collision.gameObject);

            // Use GameManager to add cherries
            if (GameManager.instance != null)
            {
                GameManager.instance.AddCherries(1);
                UpdateCherryText(); // Update the UI text
            }
            else
            {
                Debug.LogError("ItemCollector: GameManager instance not found!");
            }
        }
    }

    // Helper method to update the UI Text
    private void UpdateCherryText()
    {
        if (cherriesText != null && GameManager.instance != null)
        {
            cherriesText.text = "Cherries: " + GameManager.instance.GetCherryCount();
        }
        else
        {
            if (cherriesText == null) Debug.LogError("ItemCollector: CherriesText reference is missing!");
            if (GameManager.instance == null) Debug.LogError("ItemCollector: GameManager instance not found when trying to update text!");
        }
    }
}
