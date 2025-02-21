using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private Renderer healthBar;
    [SerializeField]
    private Material[] materials;

    void Start()
    {
        healthBar.material = materials[10];
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            DestroyShip();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.material = materials[Mathf.RoundToInt(currentHealth/10f)];
    }

    private void DestroyShip()
    {
        gameObject.SetActive(false);
    }
}
