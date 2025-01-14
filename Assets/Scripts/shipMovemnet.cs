using UnityEngine;
using Oculus; // Ensure you have the Oculus Integration package installed

public class SpaceshipController : MonoBehaviour
{
    public GameObject mover; // The joystick-like object
    public float moveSpeed = 500f; // Movement speed for the spaceship
    public float rotationSpeed = 50f; // Rotation speed
    public Transform spaceship; // Reference to the spaceship transform
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back

    private Vector3 initialMoverPosition;

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
        Vector3 movement = new Vector3(-offset.z, 0, offset.x) * moveSpeed * Time.deltaTime;

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
        // Use OVRInput to get joystick input from the right-hand controller
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        // Apply rotation based on joystick input
        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);
        spaceship.Rotate(rotation, Space.Self);
    }
}
