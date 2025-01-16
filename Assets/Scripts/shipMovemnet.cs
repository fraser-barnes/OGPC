using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    //28.81
    public GameObject mover; // The joystick-like object
    public float moveSpeed = 500f; // Movement speed for the spaceship
    public float rotationSpeed = 50f; // Rotation speed for controller input
    public Transform spaceship; // Reference to the spaceship transform
    public float moverSensitivity = 2f; // Sensitivity for mover input
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back

    private Vector3 initialMoverPositionLocal; // Mover position relative to the spaceship

    void Start()
    {
        if (mover == null || spaceship == null)
        {
            Debug.LogError("Please assign the mover and spaceship objects.");
            enabled = false;
            return;
        }

        // Store the initial local position of the mover
        initialMoverPositionLocal = spaceship.InverseTransformPoint(mover.transform.position);
    }

    void Update()
    {
        HandleMoverInput();
        HandleThrust();
        HandleRotation();
    }

    void HandleMoverInput()
{
    // Get input for movement (joystick or keyboard)
    Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
    if (input == Vector2.zero)
    {
        input.x = Input.GetAxis("Horizontal"); // A/D or Left/Right
        input.y = Input.GetAxis("Vertical");   // W/S or Up/Down
    }

    // Convert input into local space movement
    Vector3 localMovement = new Vector3(input.x, 0, input.y) * moverSensitivity * Time.deltaTime;

    // Transform local movement to world space based on spaceship's orientation
    Vector3 worldMovement = spaceship.TransformDirection(localMovement);

    // Move the orb in world space
    mover.transform.position += worldMovement;

    // Clamp the orb's position relative to the ship in local space
    Vector3 localMoverPosition = spaceship.InverseTransformPoint(mover.transform.position);
    localMoverPosition = new Vector3(
        Mathf.Clamp(localMoverPosition.x, initialMoverPositionLocal.x - 0.5f, initialMoverPositionLocal.x + 0.5f),
        initialMoverPositionLocal.y,
        Mathf.Clamp(localMoverPosition.z, initialMoverPositionLocal.z - 0.5f, initialMoverPositionLocal.z + 0.5f)
    );

    // Apply the clamped position back to world space
    mover.transform.position = spaceship.TransformPoint(localMoverPosition);

    // Debug log to verify positions
    Debug.Log($"Orb World Position: {mover.transform.position} | Local Position: {localMoverPosition}");
}


void HandleThrust()
{
    // Calculate thrust based on mover's local offset
    Vector3 localOffset = spaceship.InverseTransformPoint(mover.transform.position) - initialMoverPositionLocal;

    // WASD fallback input for thrust
    float thrustX = Input.GetAxis("Horizontal");
    float thrustZ = Input.GetAxis("Vertical");

    // Combine mover offset and WASD input
    Vector3 thrustDirection = new Vector3(
        localOffset.x + thrustX, 
        0, 
        localOffset.z + thrustZ
    );

    // Scale and apply thrust to spaceship
    Vector3 movement = thrustDirection * moveSpeed * Time.deltaTime;
    spaceship.Translate(movement, Space.World);

    // Smooth snap the mover back to its initial position
    Vector3 snappedLocalPosition = Vector3.Lerp(
        spaceship.InverseTransformPoint(mover.transform.position),
        initialMoverPositionLocal,
        Time.deltaTime * (snapBackSpeed / 2) // Lower snap speed for visibility
    );

    // Apply snapped position back to mover
    mover.transform.position = spaceship.TransformPoint(snappedLocalPosition);
}

    void HandleRotation()
    {
        // Get thumbstick input for rotation
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Apply rotation based on input
        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);
        spaceship.Rotate(rotation, Space.Self);
    }
}
