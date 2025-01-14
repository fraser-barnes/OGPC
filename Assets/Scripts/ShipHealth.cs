using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Elements")]
    public Slider healthBar;

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Ensure the health bar reflects the initial health
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    /// <summary>
    /// Reduces the ship's health.
    /// </summary>
    /// <param name="damage">Amount of damage to apply.</param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Heals the ship.
    /// </summary>
    /// <param name="amount">Amount of health to restore.</param>
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the health bar UI.
    /// </summary>
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    /// <summary>
    /// Handles the ship's destruction.
    /// </summary>
    private void Die()
    {
        Debug.Log("Ship destroyed!");
        // Add destruction logic here (e.g., animations, disabling the GameObject)
        gameObject.SetActive(false);
    }
}
