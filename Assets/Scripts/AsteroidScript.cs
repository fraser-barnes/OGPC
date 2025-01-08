using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject ship;
    private float lifetime;
    private float xTarget; // Stores the randomized x position
    private float yTarget; // Stores the randomized y position
    private float zTarget; // Stores the randomized z position
    
    void Awake(){

    }
    
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            lifetime = 0;
            gameObject.SetActive(false);
            
        }

         // Get the ship's x position and add a random offset
        xTarget = (ship.transform.position.x - transform.position.x) + Random.Range(-30f, 31f); 
        // Get the ship's y position and add a random offset
        yTarget = (ship.transform.position.y - transform.position.y) + Random.Range(-30f, 31f); 
        // Get the ship's z position and add a random offset
        zTarget = (ship.transform.position.z - transform.position.z) + Random.Range(-30f, 31f); 
        
        Vector3 targetPosition = new Vector3(xTarget, yTarget, zTarget);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 250*Time.deltaTime); 
    }
}
