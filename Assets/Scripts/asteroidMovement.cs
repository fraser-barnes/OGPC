using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject[] asteroidArr;
    private GameObject currentAsteroid;

    
    void Awake()
    {

    }

    void Update()
    {
        for(int n=0; n < asteroidArr.Length; n++){
            if(!asteroidArr[n].activeInHierarchy){
                asteroidArr[n].SetActive(true);
                asteroidArr[n].transform.position = new Vector3(Random.Range(-500f, 501f), Random.Range(-500f, 501f), Random.Range(-500f, 501f));
                currentAsteroid = asteroidArr[n];
                break;
            }
        }
       
    }
}
