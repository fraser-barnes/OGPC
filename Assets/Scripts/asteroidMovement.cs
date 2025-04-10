using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject[] asteroidArr; // Array of all asteroids
    public float asteroidSpeedMin = 50f;
    public float asteroidSpeedMax = 150f;
    public float despawnDistance = 15000f; // Distance at which asteroids despawn
    private float asteroidCooldown = 5f; // Cooldown between spawns
    private float asteroidCooldownTimer; // Timer for cooldown
    private Transform shipTransform; // Reference to the ship's transform
    public float shipDistanceThreshold = 20000f; // Threshold distance to ship

    void Start()
    {
        // Assuming the ship is tagged as "Ship" in the scene
        shipTransform = GameObject.FindWithTag("Player").transform;
        asteroidCooldownTimer = asteroidCooldown; // Initialize cooldown timer
    }

    void Update()
    {
        // Decrease the cooldown timer by the elapsed time
        asteroidCooldownTimer -= Time.deltaTime;

        // Spawn new asteroid when the cooldown is over
        if (asteroidCooldownTimer <= 0f)
        {
            for (int n = 0; n < asteroidArr.Length; n++)
            {
                // Check if asteroid is inactive
                if (!asteroidArr[n].activeInHierarchy)
                {
                    // Reset cooldown timer
                    asteroidCooldownTimer = asteroidCooldown;

                    // Set random spawn position
                    Vector3 randomDirection = Random.onUnitSphere;
                    float spawnDistance = Random.Range(5000f, 10000f);
                    Vector3 spawnPosition = transform.position + randomDirection * spawnDistance;

                    // Set random scale
                    float randomScale = Random.Range(200f, 500f);
                    Vector3 asteroidScale = new Vector3(randomScale, randomScale, randomScale);

                    asteroidArr[n].transform.position = spawnPosition;
                    asteroidArr[n].transform.localScale = asteroidScale;
                    asteroidArr[n].SetActive(true);

                    // Initialize asteroid movement
                    AsteroidScript asteroidScript = asteroidArr[n].GetComponent<AsteroidScript>();
                    if (asteroidScript != null)
                    {
                        asteroidScript.InitializeMovement();
                    }

                    break; // Exit the loop after spawning one asteroid
                }
            }
        }

        // Check if asteroids are too far from the ship and deactivate them
        for (int n = 0; n < asteroidArr.Length; n++)
        {
            if (asteroidArr[n].activeInHierarchy)
            {
                float distanceToShip = Vector3.Distance(asteroidArr[n].transform.position, shipTransform.position);

                if (distanceToShip > shipDistanceThreshold)
                {
                    // Deactivate asteroid if it's too far from the ship
                    asteroidArr[n].SetActive(false);
                }
            }
        }
    }
}
