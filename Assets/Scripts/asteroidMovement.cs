using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject[] asteroidArr;
    private GameObject currentAsteroid;
    private float asteroidCooldown;

    void Update()
    {
        asteroidCooldown += Time.deltaTime;
        for (int n = 0; n < asteroidArr.Length; n++)
        {
            if (!asteroidArr[n].activeInHierarchy && asteroidCooldown > (17f / 21f))
            {
                asteroidCooldown = 0;

                // Set a random spawn position
                Vector3 spawnPosition = new Vector3(
                    Random.Range(transform.position.x - 2000, transform.position.x + 2001),
                    Random.Range(transform.position.y - 2000f, transform.position.y + 2001f),
                    Random.Range(transform.position.z + 2000, transform.position.z + 5001)
                );

                // Set a random scale between 200 and 250
                float randomScale = Random.Range(200f, 350f);
                Vector3 asteroidScale = new Vector3(randomScale, randomScale, randomScale);

                // Activate and configure the asteroid
                asteroidArr[n].transform.position = spawnPosition;
                asteroidArr[n].transform.localScale = asteroidScale;
                asteroidArr[n].SetActive(true);

                currentAsteroid = asteroidArr[n];
                break;
            }
        }
    }
}
