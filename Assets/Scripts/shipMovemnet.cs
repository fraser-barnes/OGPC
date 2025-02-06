using UnityEditor;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    //28.81
    public GameObject mover; // The orb object
    public float moveSpeed = 500f; // Movement speed for the spaceship
    public float rotationSpeed = 50f; // Rotation speed for controller input
    public Transform spaceship; // Reference to the spaceship transform
    public float moverSensitivity = 2f; // Sensitivity for mover input
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back
    public GameObject blackHole; //black hole for position
    private Vector3 moveDirection;
    private Vector3 targetPosition;

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
        targetPosition = blackHole.transform.position;
        moveDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        HandleMoverInput();
        HandleThrust();
        HandleRotation();
        handleBlackHole();
    }

    void handleBlackHole(){
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Mathf.Max(Vector3.Distance(transform.position, targetPosition), 0.01f); // Avoid division by zero
        float gravityForce = 1000000f / distance; // Inverse distance force
        transform.position += direction * gravityForce * Time.deltaTime;
    }
    void HandleMoverInput()
{
    // Get input for movement (joystick or WASD)
    Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
    if (input == Vector2.zero)
    {
        input.x = -Input.GetAxis("Horizontal"); // A/D keys (Left/Right)
        input.y = Input.GetAxis("Vertical");   // W/S keys (Forward/Backward)
    }

    // Desk tilt angle in radians
    float deskAngleRadians = Mathf.Deg2Rad * 28.81f;

    // Convert input into movement relative to the desk's plane
    float forwardMovementZ = input.y / Mathf.Cos(deskAngleRadians); // Forward movement adjusted for tilt
    float upwardMovementY = input.y * Mathf.Tan(deskAngleRadians);  // Vertical movement based on tilt
    Vector3 localMovement = new Vector3(
        forwardMovementZ,   // Movement along tilted plane
        upwardMovementY,    // Adjusted vertical movement
        input.x             // Lateral movement (side-to-side)
    ) * moverSensitivity * Time.deltaTime;

    // Convert local movement to world space
    Vector3 worldMovement = spaceship.TransformDirection(localMovement);

    // Apply movement
    mover.transform.position += worldMovement;

    // Clamp mover's position within a defined range
    Vector3 localMoverPosition = spaceship.InverseTransformPoint(mover.transform.position);
    localMoverPosition = new Vector3(
        Mathf.Clamp(localMoverPosition.x, initialMoverPositionLocal.x - 0.5f, initialMoverPositionLocal.x + 0.5f),
        localMoverPosition.y, // Allow vertical movement
        Mathf.Clamp(localMoverPosition.z, initialMoverPositionLocal.z - 0.5f, initialMoverPositionLocal.z + 0.5f)
    );

    // Apply clamped position
    mover.transform.position = spaceship.TransformPoint(localMoverPosition);
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
        // Get thumbstick input for yaw (left/right rotation)
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Initialize rotation vector
        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);

        // Arrow key inputs for additional rotation control
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotation.z -= rotationSpeed * Time.deltaTime; // Tilt forward
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotation.z += rotationSpeed * Time.deltaTime; // Tilt backward
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation.y -= rotationSpeed * Time.deltaTime; // Turn left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation.y += rotationSpeed * Time.deltaTime; // Turn right
        }

        // Apply rotation to the spaceship
        spaceship.Rotate(rotation, Space.Self);
    }


}
