using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject ship; // Reference to the ship
    private float lifetime;
    private Vector3 movementDirection; // Current movement direction of the asteroid
    private float speed = 500f; // Speed of the asteroid
    private float deflectionDampening = 0.5f; // Reduces momentum after deflection
    private float randomOffsetRange = 100f; // Range for random offset

    void OnEnable()
    {
        // Calculate a randomized target relative to the ship's position
        Vector3 targetPosition = ship.transform.position + new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange)
        );

        // Determine the initial movement direction towards the randomized target
        movementDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 20)
        {
            lifetime = 0;
            gameObject.SetActive(false);
        }

        // Move the asteroid in the current direction
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the asteroid collided with the ship
        if (collision.gameObject == ship)
        {
            Debug.Log("Asteroid collided with the ship!");

            // Get the collision normal (direction of deflection)
            Vector3 collisionNormal = collision.contacts[0].normal;

            // Reflect the current movement direction based on the collision normal
            movementDirection = Vector3.Reflect(movementDirection, collisionNormal).normalized;

            // Reduce the speed to simulate loss of momentum
            speed *= deflectionDampening;

            // Ensure the asteroid moves away from the collision point
            transform.position += collisionNormal * 0.1f;
        }
        // Check if the asteroid collided with another asteroid
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroid collided with another asteroid!");

            // Get the collision normal (direction of deflection)
            Vector3 collisionNormal = collision.contacts[0].normal;

            // Reflect the current movement direction based on the collision normal
            movementDirection = Vector3.Reflect(movementDirection, collisionNormal).normalized;

            // Slightly reduce the speed to simulate energy loss during the collision
            speed *= deflectionDampening;

            // Ensure the asteroid moves away from the collision point
            transform.position += collisionNormal * 0.1f;
        }
    }
}
