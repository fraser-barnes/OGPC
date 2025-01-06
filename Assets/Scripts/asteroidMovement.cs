using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Transform target; // Target the asteroid will move toward
    [SerializeField] private GameObject ship; // Reference to the ship GameObject
    private float xTarget; // Stores the randomized x position

    void Update()
    {
        // Ensure the ship reference is valid
        if (ship != null)
        {
            // Get the ship's x position and add a random offset
            xTarget = ship.transform.position.x + Random.Range(-30f, 31f);

            // Move the asteroid horizontally toward the randomized xTarget
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(newPosition.x, xTarget, Time.deltaTime);
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Ship GameObject is not assigned!");
        }
    }
}
