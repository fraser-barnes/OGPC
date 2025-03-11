using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject ship; // Reference to the ship
    private Vector3 movementDirection; // Initial movement direction
    private Vector3 initialPosition; // Initial position of the asteroid
    private float speed = 200f; // Speed of the asteroid
    private float deflectionDampening = 0.5f; // Reduces momentum after deflection
    private float spawnDistanceMin = 5000f; // Minimum spawn distance from the ship
    private float spawnDistanceMax = 10000f; // Maximum spawn distance from the ship
    private float despawnDistanceThreshold = 10000f; // Despawn threshold if asteroid is too far from the ship

    private bool collidedWithShip = false; // Flag for collision with the ship

    void OnEnable()
    {
        SetInitialDirection(); // Set movement direction upon spawn
    }

    void Update()
    {
        // If the asteroid is too far from the ship, reset it
        if (Vector3.Distance(transform.position, ship.transform.position) > despawnDistanceThreshold)
        {
            ResetAsteroid();
        }

        // Update movement direction toward the ship's current position
        UpdateMovementDirection();

        // Move asteroid in the updated direction
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ship)
        {
            Debug.Log("Asteroid collided with the ship!");
            collidedWithShip = true;
            ResetAsteroid(); // Reset asteroid immediately upon collision
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroid collided with another asteroid!");

            // Reflect direction based on collision normal
            Vector3 collisionNormal = collision.contacts[0].normal;
            movementDirection = Vector3.Reflect(movementDirection, collisionNormal).normalized;

            // Reduce speed slightly
            speed *= deflectionDampening;

            // Move slightly away from the collision point
            transform.position += collisionNormal * 0.1f;
        }
    }

    private void SetInitialDirection()
    {
        // Spawn in all directions around the ship at a further distance
        Vector3 randomDirection = Random.onUnitSphere; // 360-degree random direction
        float spawnDistance = Random.Range(spawnDistanceMin, spawnDistanceMax);
        transform.position = ship.transform.position + randomDirection * spawnDistance;

        // Set initial movement direction (towards the ship)
        movementDirection = (ship.transform.position - transform.position).normalized;
        initialPosition = transform.position; // Store initial spawn position
    }

    private void UpdateMovementDirection()
    {
        // Continuously update the direction toward the current ship position
        movementDirection = (ship.transform.position - transform.position).normalized;
    }

    private void ResetAsteroid()
    {
        // Reset asteroid properties and deactivate
        gameObject.SetActive(false); // Deactivate asteroid when despawned
    }
}
