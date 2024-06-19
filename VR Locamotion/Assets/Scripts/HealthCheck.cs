using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthCheck : MonoBehaviour
{
    public Image playerHealthBar;  // Reference to the player's health bar image
    public TMP_Text playerHealthText;  // Reference to the player's health TMP text
    public Image zombieHealthBar;  // Reference to the zombie's health bar image
    public TMP_Text zombieHealthText;  // Reference to the zombie's health TMP text

    public HealthBar playerHealthScript;  // Reference to the player health script
    public ZombieHealth zombieHealthScript;  // Reference to the zombie health script

    void Update()
    {
        if (playerHealthScript != null && zombieHealthScript != null)
        {
            float playerHealth = playerHealthScript.currentHealth;
            float zombieHealth = zombieHealthScript.currentHealth;

            // Update UI elements to reflect the current health
            playerHealthText.text = playerHealth.ToString("F0"); // Display health as whole number
            zombieHealthText.text = zombieHealth.ToString("F0"); // Display health as whole number

            Debug.Log("Player Health: " + playerHealth);
            Debug.Log("Zombie Health: " + zombieHealth);

            if (playerHealth <= 0)
            {
                Debug.Log("Player Health is 0 or less, loading Lose scene.");
                OpenLoseScene();
            }
            else if (zombieHealth <= 0)
            {
                Debug.Log("Zombie Health is 0 or less, loading Win scene.");
                OpenWinScene();
            }
        }
        else
        {
            Debug.LogError("PlayerHealth or ZombieHealth reference is missing.");
        }
    }

    void OpenLoseScene()
    {
        // Assuming "Lose" is the name of your lose scene
        SceneManager.LoadScene("Lose");
    }

    void OpenWinScene()
    {
        // Assuming "Win" is the name of your win scene
        SceneManager.LoadScene("Win");
    }
}
