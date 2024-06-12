using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Public variable to set the scene name in the Inspector
    public string sceneName;

    // This method will be called when the button is clicked
    public void OnStartButtonClicked()
    {
        // Check if the scene name is not empty or null
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }
}
