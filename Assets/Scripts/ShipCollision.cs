using UnityEngine;

public class ShipCollision : MonoBehaviour
{
   public ShipHealth shipHealth; // Reference to the ShipHealth script
   public int damageAmount = 10; // Damage caused by the asteroid

   private void OnCollisionEnter(Collision collision)
   {
        // Check if the collision is with an asteroid
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            shipHealth.TakeDamage(damageAmount);
        } else if (collision.gameObject.CompareTag("BlackHole"))
        {
            shipHealth.TakeDamage(1000);
        }
   }
}
