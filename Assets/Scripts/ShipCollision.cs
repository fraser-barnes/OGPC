using UnityEngine;

public class ShipCollision : MonoBehaviour
{
   public ShipHealth shipHealth; // Reference to the ShipHealth script
   public int damageAmount = 1; // Damage caused by the asteroid

   private void OnCollisionEnter(Collision collision)
   {
       // Check if the collision is with an asteroid
       if (collision.gameObject.CompareTag("Asteroid"))
       {

         Debug.Log("collided"); // Print to the console
           // Call the TakeDamage method on the ShipHealth script
           if (shipHealth != null)
           {
               shipHealth.TakeDamage(damageAmount);
           }

           // Optionally, destroy the asteroid
          // collision.gameObject.SetActive(false);

       }
   }

   private void OnTriggerEnter(Collider other)
   {
       // Alternatively, handle trigger collisions if using trigger colliders
       if (other.CompareTag("Asteroid"))
       {

         Debug.Log("collided"); // Print to the console
           if (shipHealth != null)
           {
               shipHealth.TakeDamage(damageAmount);
           }

           //other.gameObject.SetActive(false);
       }
   }
}
