using UnityEngine;

public class BlasterController : MonoBehaviour
{
    // Define variables for blaster settings
    public GameObject blasterPrefab; // The projectile to be fired
    public Transform spawnPoint; // Where the blaster spawns
    public float blasterSpeed = 50f; // Base speed of the projectile
    public float extraSpeed = 20f; // Extra speed added on fire

    // Reference to the ship's Rigidbody for velocity
    public Rigidbody shipRigidbody;

    // New serializable variable to adjust the firing angle relative to the ship
    [Header("Firing Angle Settings")]
    [Range(-90f, 90f)]
    public float fireAngle = 0f; // Angle in degrees relative to the ship's forward direction

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Fire when spacebar is pressed
        {
            FireBlaster();
        }
    }

    void FireBlaster()
    {
        if (blasterPrefab != null && spawnPoint != null)
        {
            // Instantiate the blaster at the spawn point
            GameObject blaster = Instantiate(blasterPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get or add a Rigidbody to the blaster
            Rigidbody blasterRb = blaster.GetComponent<Rigidbody>();
            if (blasterRb == null)
            {
                blasterRb = blaster.AddComponent<Rigidbody>();
            }

            // Ensure Rigidbody has no drag (so it keeps moving)
            blasterRb.drag = 0;
            blasterRb.angularDrag = 0;
            blasterRb.useGravity = false; // Assuming blasters move in space

            // Calculate the direction to fire the blaster relative to the ship's current rotation
            Vector3 fireDirection = spawnPoint.forward;

            // Apply the rotation based on the fireAngle (around the Y-axis for horizontal rotation)
            fireDirection = Quaternion.Euler(0, fireAngle, 0) * fireDirection;

            // Calculate total velocity (ship velocity + additional speed in the adjusted forward direction)
            Vector3 totalVelocity = shipRigidbody.velocity + fireDirection * (blasterSpeed + extraSpeed);

            // Apply velocity to the blaster (ensuring continuous movement)
            blasterRb.velocity = totalVelocity;
        }
    }
}
