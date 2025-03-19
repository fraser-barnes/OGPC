using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Vector3 movementDirection; // Initial movement direction
    private float speed;
    private float deflectionDampening = 0.5f; // Reduces momentum after deflection
    private float spawnDistanceMin = 5000f; // Minimum spawn distance
    private float spawnDistanceMax = 10000f; // Maximum spawn distance
    private float despawnDistanceThreshold = 15000f; // Despawn threshold if asteroid is too far from origin

    public void InitializeMovement()
    {
        movementDirection = Random.onUnitSphere.normalized; // Random movement direction
        speed = Random.Range(50f, 150f); // Random speed within range
    }

    void Update()
    {
        // Move asteroid in its set direction
        transform.position += movementDirection * speed * Time.deltaTime;

        if (transform.position.magnitude > despawnDistanceThreshold)
        {
            ResetAsteroid();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
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

    private void ResetAsteroid()
    {
        // Reset asteroid properties and deactivate
        gameObject.SetActive(false); // Deactivate asteroid when despawned
    }
}
