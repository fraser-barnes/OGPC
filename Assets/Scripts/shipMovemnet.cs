using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class SpaceshipController : MonoBehaviour
{
    public GameObject mover; // The joystick-like object
    public float moveSpeed = 500f; // Movement speed for the spaceship
    public float rotationSpeed = 50f; // Rotation speed
    public Transform spaceship; // Reference to the spaceship transform
    public float moverSensitivity = 2f; // Sensitivity for mover input
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back

    private Vector3 initialMoverPositionLocal; // Mover position relative to spaceship

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

        // Ensure the OVRCameraRig is in the scene
        OVRCameraRig cameraRig = FindObjectOfType<OVRCameraRig>();
        if (cameraRig == null)
        {
            Debug.LogError("OVRCameraRig not found. Please ensure it's added to the scene.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        HandleMoverInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleMoverInput()
    {
        // Get thumbstick input from the right-hand controller
        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Fallback to arrow keys for desktop debugging
        if (thumbstickInput == Vector2.zero)
        {
            thumbstickInput.x = Input.GetAxis("Horizontal"); // Left/right arrow keys
            thumbstickInput.y = Input.GetAxis("Vertical");   // Up/down arrow keys
        }

        // Correct the movement to align with the spaceship's local coordinate system
        Vector3 correctedMovement = spaceship.TransformDirection(new Vector3(thumbstickInput.y, 0, -thumbstickInput.x)) 
                                    * moverSensitivity * Time.deltaTime;

        // Update the mover's position relative to the spaceship
        mover.transform.position += correctedMovement;

        // Clamp the mover's position relative to the spaceship
        Vector3 localMoverPosition = spaceship.InverseTransformPoint(mover.transform.position);
        localMoverPosition = new Vector3(
            Mathf.Clamp(localMoverPosition.x, initialMoverPositionLocal.x - 0.5f, initialMoverPositionLocal.x + 0.5f),
            initialMoverPositionLocal.y,
            Mathf.Clamp(localMoverPosition.z, initialMoverPositionLocal.z - 0.5f, initialMoverPositionLocal.z + 0.5f)
        );

        // Update mover's world position after clamping
        mover.transform.position = spaceship.TransformPoint(localMoverPosition);
    }

    void HandleMovement()
    {
        // Calculate offset from the initial local position
        Vector3 localOffset = spaceship.InverseTransformPoint(mover.transform.position) - initialMoverPositionLocal;

        // Scale the offset to movement
        Vector3 movement = new Vector3(-localOffset.z, 0, localOffset.x) * moveSpeed * Time.deltaTime;

        // Apply movement to the spaceship
        spaceship.Translate(movement, Space.World);

        // Snap the mover back to its initial local position smoothly
        Vector3 snappedLocalPosition = Vector3.Lerp(
            spaceship.InverseTransformPoint(mover.transform.position),
            initialMoverPositionLocal,
            Time.deltaTime * snapBackSpeed
        );

        // Update the mover's world position after snapping
        mover.transform.position = spaceship.TransformPoint(snappedLocalPosition);
    }

    void HandleRotation()
    {
        // Get thumbstick input for rotation
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        // Fallback to arrow keys for desktop debugging (rotation)
        if (rotationInput == Vector2.zero)
        {
            rotationInput.x = Input.GetAxis("Horizontal"); // Left/right arrow keys
        }

        // Apply rotation based on input
        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);
        spaceship.Rotate(rotation, Space.Self);
    }
}
