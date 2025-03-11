using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject[] asteroidArr; // Array of all asteroids
    private float asteroidCooldown;

    void Update()
    {
        asteroidCooldown += Time.deltaTime;

        // Spawn asteroids if they are not already active
        for (int n = 0; n < asteroidArr.Length; n++)
        {
            if (!asteroidArr[n].activeInHierarchy && asteroidCooldown > 0)
            {
                asteroidCooldown = 0;

                // Set a random spawn position within the range of 5,000f to 10,000f from the ship
                Vector3 randomDirection = Random.onUnitSphere; // Random direction in all directions
                float spawnDistance = Random.Range(5000f, 10000f); // Spawn distance between 5000f and 10000f
                Vector3 spawnPosition = transform.position + randomDirection * spawnDistance;

                // Set a random scale between 200f and 350f
                float randomScale = Random.Range(200f, 350f);
                Vector3 asteroidScale = new Vector3(randomScale, randomScale, randomScale);

                // Configure and activate the asteroid immediately
                asteroidArr[n].transform.position = spawnPosition;
                asteroidArr[n].transform.localScale = asteroidScale;
                asteroidArr[n].SetActive(true);  // Immediately set the asteroid active

                break;
            }
        }
    }
}
