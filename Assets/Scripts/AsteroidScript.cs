using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject ship;
    private float lifetime;
    private float xTarget; // Stores the randomized x position
    private float yTarget; // Stores the randomized y position
    private float zTarget; // Stores the randomized z position
    
    void Awake(){
         // Get the ship's x position and add a random offset
        xTarget = 1.5f*((transform.position.x - ship.transform.position.x) + Random.Range(-100f, 101f)); 
        // Get the ship's y position and add a random offset
        yTarget = 1.5f*((transform.position.y - ship.transform.position.y) + Random.Range(-100f, 101f)); 
        // Get the ship's z position and add a random offset
        zTarget = 1.5f*((transform.position.z - ship.transform.position.z) + Random.Range(-100f, 101f)); 
    }
    
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 20)
        {
            lifetime = 0;
            gameObject.SetActive(false);
            
        }
        
        Vector3 targetPosition = new Vector3(transform.position.x - xTarget, transform.position.y - yTarget, transform.position.z -zTarget);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 500*Time.deltaTime); 
    }
}
