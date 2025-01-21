using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject[] asteroidArr;
    private GameObject currentAsteroid;
    private float asteroidCooldown;

    void Awake()
    {

    }

    void Update()
    {
        asteroidCooldown += Time.deltaTime;
        for(int n=0; n < asteroidArr.Length; n++){
            if(!asteroidArr[n].activeInHierarchy && asteroidCooldown > (20f/11f)){
                asteroidCooldown = 0;
                asteroidArr[n].transform.position = new Vector3(Random.Range(transform.position.x-3000, transform.position.x+3001), Random.Range(transform.position.y-3000f, transform.position.y+3001f), Random.Range(transform.position.z+5000, transform.position.z+7001));
                asteroidArr[n].SetActive(true);
                currentAsteroid = asteroidArr[n];
                break;
            }
        }

    }
}
