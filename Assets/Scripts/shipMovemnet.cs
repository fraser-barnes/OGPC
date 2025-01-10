using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceshipController : MonoBehaviour
{
    public GameObject mover; // The joystick-like object
    public float moveSpeed = 500f; // Movement speed
    public float rotationSpeed = 50f; // Rotation speed
    public Transform spaceship; // Reference to the spaceship transform

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
        HandleMovement();
        HandleRotation();
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
            Time.deltaTime * 5f // Smooth snapping
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
