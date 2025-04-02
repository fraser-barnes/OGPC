using UnityEngine;

public class BlasterController : MonoBehaviour
{
    public GameObject blasterPrefab; // Assign your blaster projectile prefab here
    public Transform spawnPoint;    // Assign your spawn point transform here
    public float additionalSpeed = 10f; // Adjust this value for projectile speed

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component of the spaceship
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBlaster();
        }
    }

    void FireBlaster()
    {
        // Instantiate the blaster at the spawn point position
        GameObject blaster = Instantiate(blasterPrefab, spawnPoint.position, spawnPoint.rotation);

        // Get the Rigidbody component of the blaster
        Rigidbody blasterRb = blaster.GetComponent<Rigidbody>();

        // Calculate the total velocity (spaceship velocity + additional forward velocity)
        Vector3 totalVelocity = rb.linearVelocity + (transform.forward * additionalSpeed);

        // Apply the calculated velocity to the blaster
        blasterRb.linearVelocity = totalVelocity;
    }
}
