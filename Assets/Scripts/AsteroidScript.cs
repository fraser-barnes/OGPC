using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Vector3 movementDirection;
    private float speed;
    private float deflectionDampening = 0.5f;
    private float spawnDistanceMin = 5000f;
    private float spawnDistanceMax = 10000f;
    private float despawnDistanceThreshold = 15000f;

    private int hitCount = 0;
    private int hitsToDisable = 3;

    public void InitializeMovement()
    {
        movementDirection = Random.onUnitSphere.normalized;
        speed = Random.Range(50f, 150f);
        hitCount = 0;
        gameObject.SetActive(true); // Reactivate if coming from pool
    }

    void Update()
    {
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
            Vector3 collisionNormal = collision.contacts[0].normal;
            movementDirection = Vector3.Reflect(movementDirection, collisionNormal).normalized;
            speed *= deflectionDampening;
            transform.position += collisionNormal * 0.1f;
        }
        else if (collision.gameObject.CompareTag("Blaster"))
{
    hitCount++;
    Debug.Log($"Asteroid hit! Count: {hitCount}");

    Destroy(collision.gameObject);

    if (hitCount >= hitsToDisable)
    {
        Debug.Log("Asteroid reached max hits — deactivating.");
        ResetAsteroid();
    }
}

    }

    private void ResetAsteroid()
{
    Debug.Log("ResetAsteroid() called.");
    hitCount = 0;
    gameObject.SetActive(false);
}

}
