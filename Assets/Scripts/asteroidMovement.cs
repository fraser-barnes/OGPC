using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Transform target; // Target the asteroid will move toward
    [SerializeField] private GameObject ship; // Reference to the ship GameObject
   
    private float xTarget; // Stores the randomized x position
    private float yTarget; // Stores the randomized y position
    private float zTarget; // Stores the randomized z position
    
    void Awake()
    {
        // Get the ship's x position and add a random offset
        xTarget = ship.transform.position.x + Random.Range(-30f, 31f); 
        // Get the ship's y position and add a random offset
        yTarget = ship.transform.position.y + Random.Range(-30f, 31f); 
        // Get the ship's z position and add a random offset
        zTarget = ship.transform.position.z + Random.Range(-30f, 31f); 
    }

    void Update()
    {
        // Ensure the ship reference is valid
        if (ship != null)
        {
            // Move the asteroid horizontally toward the randomized xTarget
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(newPosition.x, xTarget, Time.deltaTime);
            newPosition.y = Mathf.Lerp(newPosition.y, yTarget, Time.deltaTime);
            newPosition.z = Mathf.Lerp(newPosition.z, zTarget, Time.deltaTime);
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Ship GameObject is not assigned!");
        }
    }
}
