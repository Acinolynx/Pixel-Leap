using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Renamed class to match filename and purpose
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance; // Singleton instance

    public Animator transitionAnimator; // Renamed variable - Assign this in Inspector if possible!
    public float transitionTime = 1f;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the loader across scenes

            // --- Animator Check (Moved to correct location) ---
            if (transitionAnimator == null)
            {
                Debug.LogWarning("LevelLoader: Transition Animator not assigned in Inspector. Trying to find automatically...");
                // Try finding it on this GameObject first
                transitionAnimator = GetComponent<Animator>();

                // If not found on this GameObject, try finding it in children
                if (transitionAnimator == null)
                {
                    // Include inactive children in case the transition panel starts inactive
                    transitionAnimator = GetComponentInChildren<Animator>(true);
                }

                // Final check and error if still not found
                if (transitionAnimator == null)
                {
                    Debug.LogError("LevelLoader: Could not find Animator component on this GameObject or its children. Please assign 'Transition Animator' in the Inspector!");
                }
                else
                {
                    Debug.Log("LevelLoader: Found Animator component automatically: " + transitionAnimator.gameObject.name);
                }
            }
            else
            {
                Debug.Log("LevelLoader: Animator assigned in Inspector: " + transitionAnimator.gameObject.name);
            }
            // ---------------------

        }
        else
        {
            Destroy(gameObject); // Destroy duplicate loaders
        }
    }

    // Method to load the next level in build index
    public void LoadNextLevel()
    {
        // Use the new coroutine that accepts index
        StartCoroutine(LoadLevelByIndexCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // New method to load a specific scene by name
    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadLevelByNameCoroutine(sceneName));
    }

    // Coroutine to load level by build index
    IEnumerator LoadLevelByIndexCoroutine(int levelIndex)
    {
        if (transitionAnimator == null) {
            Debug.LogError("LevelLoader: transitionAnimator is NULL before SetTrigger (ByIndex)!");
            yield break; // Stop if animator is missing
        }
        Debug.Log("LevelLoader: Triggering 'Start' animation (ByIndex).");
        transitionAnimator.SetTrigger("Start");

        // wait for animation to finish
        Debug.Log("LevelLoader: Waiting for transition time (ByIndex)...");
        yield return new WaitForSeconds(transitionTime);

        // load the level
        // Check if the next level index is valid
        if (levelIndex < SceneManager.sceneCountInBuildSettings)
        {
             Debug.Log("LevelLoader: Finished waiting. Loading scene index: " + levelIndex);
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogWarning("LevelLoader: Tried to load invalid scene index: " + levelIndex + ". Check Build Settings.");
            // Optionally load the main menu or first level instead
            // SceneManager.LoadScene(0); // Example: Load first scene
        }
    }

    // New coroutine to load level by name
    IEnumerator LoadLevelByNameCoroutine(string sceneName)
    {
        if (transitionAnimator == null) {
            Debug.LogError("LevelLoader: transitionAnimator is NULL before SetTrigger (ByName)!");
            yield break; // Stop if animator is missing
        }
        // --- Add detailed logs before triggering ---
        float currentTransitionTime = this.transitionTime; // Get current value
        string currentStateName = "Unknown";
        if (transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Crossfade_Start")) {
             currentStateName = "Crossfade_Start";
        } else if (transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName("Crossfade_End")) {
             currentStateName = "Crossfade_End";
        } else {
             // Attempt to get the hash if name doesn't match known states
             int stateHash = transitionAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash;
             currentStateName = $"Unknown (Hash: {stateHash})";
             Debug.LogWarning($"LevelLoader: Animator is in an unexpected state: {currentStateName}");
        }

        Debug.Log($"LevelLoader: Before Trigger (ByName) - Scene: {sceneName}, Current State: {currentStateName}, Transition Time Value: {currentTransitionTime}");
        // -----------------------------------------

        if (currentTransitionTime <= 0) {
             Debug.LogWarning("LevelLoader: transitionTime is 0 or less. Skipping wait and animation trigger.");
             SceneManager.LoadScene(sceneName); // Load immediately if time is 0
             yield break; // Exit coroutine
        }

        Debug.Log("LevelLoader: Triggering 'Start' animation for scene: " + sceneName);
        // Ensure the trigger is reset before setting it, just in case
        transitionAnimator.ResetTrigger("Start");
        transitionAnimator.SetTrigger("Start");
        // Force an immediate update of the animator state - might help in edge cases
        transitionAnimator.Update(0f);


        // wait for animation to finish
        Debug.Log("LevelLoader: Waiting for transition time (ByName)...");
        yield return new WaitForSeconds(currentTransitionTime); // Use the captured value
        Debug.Log("LevelLoader: Finished waiting. Loading scene: " + sceneName);

        // load the level
        SceneManager.LoadScene(sceneName);
    }
}
