using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;
    [SerializeField]
    private Material healthBarMat;
    [SerializeField]
    private Texture[] textures;

    void Start()
    {
        healthBarMat.mainTexture = textures[10];
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
        healthBarMat.mainTexture = textures[Mathf.RoundToInt(currentHealth/10f)];
    }

    private void DestroyShip()
    {
        gameObject.SetActive(false);
    }
}
