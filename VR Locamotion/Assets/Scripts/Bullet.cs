using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Canvas healthBarCanvas; // Reference to the health bar canvas to update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // Check if the collided GameObject is the bullet prefab instance
            if (bulletPrefab != null && collision.gameObject == bulletPrefab)
            {
                ZombieHealth zombieHealth = collision.gameObject.GetComponent<ZombieHealth>();
                if (zombieHealth != null)
                {
                    zombieHealth.TakeDamage(damage);

                    // Update the health bar UI if the canvas reference is set
                    if (healthBarCanvas != null)
                    {
                        UpdateHealthBarUI(zombieHealth);
                    }
                }

                // Destroy the bullet prefab instance after collision
                Destroy(bulletPrefab);
            }
        }

        // Destroy the current bullet instance after collision
        Destroy(gameObject);
    }

    private void UpdateHealthBarUI(ZombieHealth zombieHealth)
    {
        // Find the health bar slider in the canvas and update it
        Slider healthBarSlider = healthBarCanvas.GetComponentInChildren<Slider>();
        if (healthBarSlider != null)
        {
            healthBarSlider.value = (float)zombieHealth.currentHealth / zombieHealth.maxHealth;
        }
    }
}
