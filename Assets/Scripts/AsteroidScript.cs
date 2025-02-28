using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject ship; // Reference to the ship
    private float lifetime;
    private float despawnTimer; // Timer for despawning after collision
    private bool collidedWithShip = false; // Flag for collision with the ship
    private Vector3 movementDirection; // Initial movement direction
    private float speed = 3000f; // Increased speed (previously 500f)
    private float deflectionDampening = 0.5f; // Reduces momentum after deflection
    private float randomOffsetRange = 500f; // Range for random offset

    void OnEnable()
    {
        SetInitialDirection(); // Set direction only when the asteroid spawns
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        // Despawn asteroid after 40 seconds
        if (lifetime > 40)
        {
            ResetAsteroid();
        }

        // If collided with the ship, start despawn countdown
        if (collidedWithShip)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer >= 1f)
            {
                ResetAsteroid();
            }
        }

        // Move asteroid in a straight line
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ship)
        {
            Debug.Log("Asteroid collided with the ship!");
            collidedWithShip = true;
            despawnTimer = 0f; // Start despawn timer
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
        // Randomized target relative to the ship's position
        Vector3 targetPosition = ship.transform.position + new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange)
        );

        // Set initial movement direction
        movementDirection = (targetPosition - transform.position).normalized;
    }

    private void ResetAsteroid()
    {
        // Reset asteroid properties and deactivate
        lifetime = 0;
        collidedWithShip = false;
        gameObject.SetActive(false);
    }
}
