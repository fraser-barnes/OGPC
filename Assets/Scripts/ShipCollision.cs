using UnityEngine;

public class ShipCollision : MonoBehaviour
{
   public ShipHealth shipHealth; // Reference to the ShipHealth script
   public float damageAmount = 20f; // Damage caused by the asteroid

   private void OnCollisionEnter(Collision collision)
   {
       // Check if the collision is with an asteroid
       if (collision.gameObject.CompareTag("Asteroid"))
       {
           // Call the TakeDamage method on the ShipHealth script
           if (shipHealth != null)
           {
               shipHealth.TakeDamage(damageAmount);
           }

           // Optionally, destroy the asteroid
           Destroy(collision.gameObject);
       }
   }

   private void OnTriggerEnter(Collider other)
   {
       // Alternatively, handle trigger collisions if using trigger colliders
       if (other.CompareTag("Asteroid"))
       {
           if (shipHealth != null)
           {
               shipHealth.TakeDamage(damageAmount);
           }

           Destroy(other.gameObject);
       }
   }
}
