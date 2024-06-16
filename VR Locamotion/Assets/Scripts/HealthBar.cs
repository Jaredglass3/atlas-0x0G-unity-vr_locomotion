using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image healthFillImage;
    public TMP_Text healthText; // Add this line
    private float currentHealth;
    private float maxHealth;

    private void Start()
    {
        maxHealth = 100f; // Example maximum health
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthFillImage.fillAmount = currentHealth / maxHealth;
        healthText.text = "Health: " + currentHealth.ToString("F0"); // Update the text
    }
}
