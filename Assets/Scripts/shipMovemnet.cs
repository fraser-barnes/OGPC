using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceshipController : MonoBehaviour
{
    public GameObject mover; // The joystick-like object
    public float moveSpeed = 500f; // Movement speed for the spaceship
    public float rotationSpeed = 50f; // Rotation speed
    public Transform spaceship; // Reference to the spaceship transform
    public float moverSensitivity = 1f; // Sensitivity for keyboard input on the mover
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back

    private Vector3 initialMoverPosition;
    private XRController rightHandController;

    void Start()
    {
        if (mover == null || spaceship == null)
        {
            Debug.LogError("Please assign the mover and spaceship objects.");
            enabled = false;
            return;
        }

        // Store the initial position of the mover for snapping back
        initialMoverPosition = mover.transform.localPosition;

        // Find the right-hand controller (adjust as needed based on your setup)
        rightHandController = FindRightHandController();
    }

    void Update()
    {
        HandleMoverInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleMoverInput()
    {
        // Get keyboard input for WASD movement
        float inputWASD_X = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float inputWASD_Y = Input.GetAxis("Vertical");   // W/S or Up/Down arrows

        // Remap WASD input to the correct axis in your game world
        Vector3 correctedMovement = new Vector3(inputWASD_Y, 0, -inputWASD_X) * moverSensitivity * Time.deltaTime;

        // Update the mover's position based on the corrected movement
        mover.transform.localPosition += correctedMovement;

        // Clamp the mover's position to a specific range (if needed)
        mover.transform.localPosition = new Vector3(
            Mathf.Clamp(mover.transform.localPosition.x, initialMoverPosition.x - 0.5f, initialMoverPosition.x + 0.5f),
            initialMoverPosition.y,
            Mathf.Clamp(mover.transform.localPosition.z, initialMoverPosition.z - 0.5f, initialMoverPosition.z + 0.5f)
        );
    }

    void HandleMovement()
    {
        // Calculate offset from the initial position
        Vector3 offset = mover.transform.localPosition - initialMoverPosition;

        // Scale the offset to movement
        Vector3 movement = new Vector3(offset.x, 0, offset.z) * moveSpeed * Time.deltaTime;

        // Apply movement to the spaceship
        spaceship.Translate(movement, Space.World);

        // Snap the mover back to its initial position
        mover.transform.localPosition = Vector3.Lerp(
            mover.transform.localPosition, 
            initialMoverPosition, 
            Time.deltaTime * snapBackSpeed // Smooth snapping
        );
    }

    void HandleRotation()
    {
        if (rightHandController != null)
        {
            // Get joystick input from the right hand controller
            rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rotationInput);

            // Apply rotation based on joystick input
            Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);
            spaceship.Rotate(rotation, Space.Self);
        }
    }

    XRController FindRightHandController()
    {
        // Find the right-hand XRController in the scene
        var controllers = FindObjectsOfType<XRController>();
        foreach (var controller in controllers)
        {
            if (controller.controllerNode == XRNode.RightHand)
            {
                return controller;
            }
        }

        Debug.LogWarning("Right-hand controller not found.");
        return null;
    }
}
