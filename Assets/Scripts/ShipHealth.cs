using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    [SerializeField]
    private Material healthBarMat;
    [SerializeField]
    private Texture[] textures;

    void Start()
    {
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
        healthBarMat.mainTexture = textures[currentHealth];
    }

    private void DestroyShip()
    {
        gameObject.SetActive(false);
    }
}
