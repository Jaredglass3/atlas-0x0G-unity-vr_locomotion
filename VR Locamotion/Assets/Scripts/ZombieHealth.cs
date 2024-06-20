using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarImage; // Assign the health bar Image here
    public TMP_Text healthText; // Assign the TMP text component here
    private Animator animator;
    private bool isDead = false;
    private ZombieMovement zombieMovement;

    // Reference to game manager or UI manager
    public GameManager gameManager; // Assign this in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        zombieMovement = GetComponent<ZombieMovement>();
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return; // Ignore damage if the zombie is already dead

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    void Die()
    {
        isDead = true;

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Stop movement and attack
        if (zombieMovement != null)
        {
            zombieMovement.StopMovementAndAttack();
        }

        // Disable components that should not function after death
        DisableComponents();

        // Notify game manager about the kill
        if (gameManager != null)
        {
            gameManager.IncreaseKillCount();
        }
    }

    void DisableComponents()
    {
        // Disable collider
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
}
