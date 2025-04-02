using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public SpaceshipController spaceshipController;
    public ShipHealth shipHealth; // Reference to the ShipHealth script
    public int damageAmount = 10; // Damage caused by the asteroid

   private void OnCollisionEnter(Collision collision)
   {
        // Check if the collision is with an asteroid
        damageAmount = 10;
        //if (spaceshipController.GetShield()){
        //        damageAmount = 5;
        //    }
        if (collision.gameObject.CompareTag("Asteroid"))
        {


            shipHealth.TakeDamage(damageAmount);
        } else if (collision.gameObject.CompareTag("BlackHole"))
        {
            shipHealth.TakeDamage(1000);
        }
   }
}
