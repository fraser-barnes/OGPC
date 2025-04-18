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
        if (Input.GetKeyDown(KeyCode.Space) && Camera.main.GetComponent<ClickDetection>().switchesActive[2]) // Fire when spacebar is pressed and switch is on
        {
            FireBlaster();
        }
    }

    void FireBlaster()
{
    if (blasterPrefab != null && spawnPoint != null)
    {

      // Calculate firing direction
      Vector3 localDirection = Quaternion.Euler(0f, fireAngle, 0f) * Vector3.forward;
      Vector3 worldDirection = spawnPoint.TransformDirection(localDirection);
      // Combine ship velocity and blaster velocity
      Vector3 totalVelocity = shipRigidbody.linearVelocity + worldDirection * (blasterSpeed + extraSpeed);

        // Offset the spawn position slightly in front of the spawn point
        Vector3 spawnOffset = spawnPoint.forward * 15f; // Change 1f to push it more/less
        Vector3 spawnPosition = spawnPoint.position + totalVelocity * 1/5;

        // Instantiate the blaster at the offset position
        GameObject blaster = Instantiate(blasterPrefab, spawnPosition, spawnPoint.rotation);

        // Rotate the blaster 90 degrees around its own Z-axis
        blaster.transform.rotation *= Quaternion.Euler(0f, 0f, 90f);

        // Get or add a Rigidbody to the blaster
        Rigidbody blasterRb = blaster.GetComponent<Rigidbody>();
        if (blasterRb == null)
        {
            blasterRb = blaster.AddComponent<Rigidbody>();
        }

        // Ensure Rigidbody has no drag
        blasterRb.linearDamping = 0;
        blasterRb.angularDamping = 0;
        blasterRb.useGravity = false;




        // Apply velocity
        blasterRb.linearVelocity = totalVelocity;

        // Destroy after 30 seconds
        Destroy(blaster, 30f);
    }
}


}
