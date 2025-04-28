using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;

public class SpaceshipController : MonoBehaviour
{
    public GameObject mover; // The orb object
    public float moveSpeed = 1000f; // Doubled movement speed
    public float rotationSpeed = 100f; // Increased rotation speed
    public Transform spaceship; // Reference to the spaceship transform
    public float moverSensitivity = 2f; // Sensitivity for mover input
    public float snapBackSpeed = 5f; // Speed at which the mover snaps back
    public GameObject blackHole; // Black hole for position

    private Vector3 moveDirection;
    private Vector3 targetPosition;
    private int negativeSign;
    private int blackHoleStartX;
    private int blackHoleStartY;
    private int blackHoleStartZ;

    private bool shieldDebounce = false;
    private Vector3 initialMoverPositionLocal; // Mover position relative to the spaceship

    private Camera cam;
    private bool[] switchesActive;

    private OVRCameraRig cameraRig;
    public OVRInput.Controller controllerType = OVRInput.Controller.LTouch;

    void Start()
    {
        if (mover == null || spaceship == null)
        {
            Debug.LogError("Please assign the mover and spaceship objects.");
            enabled = false;
            return;
        }

        cameraRig = GetComponent<OVRCameraRig>();
        if (cameraRig == null)
        {
            Debug.LogError("OVRCameraRig not found!");
        }

        // Spawn the black hole at a random or predefined position
        negativeSign = Random.Range(1, 3);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            blackHoleStartX = 5000;
            blackHoleStartY = 5000;
            blackHoleStartZ = 5000;
        }
        else if (negativeSign == 1)
        {
            blackHoleStartX = Random.Range(25000, 35000);
            blackHoleStartY = Random.Range(25000, 35000);
            blackHoleStartZ = Random.Range(25000, 35000);
        }
        else
        {
            blackHoleStartX = -Random.Range(25000, 35000);
            blackHoleStartY = -Random.Range(25000, 35000);
            blackHoleStartZ = -Random.Range(25000, 35000);
        }

        blackHole.transform.position = new Vector3(blackHoleStartX, blackHoleStartY, blackHoleStartZ);

        // Store the initial local position of the mover
        initialMoverPositionLocal = spaceship.InverseTransformPoint(mover.transform.position);
        targetPosition = blackHole.transform.position;
        moveDirection = (targetPosition - transform.position).normalized;

        cam = Camera.main;
    }

    void Update()
    {
        HandleMoverInput();
        HandleThrust();
        HandleRotation();
        HandleBlackHole();

        switchesActive = cam.GetComponent<ClickDetection>().switchesActive;

        // Check if ClickDetection has a shieldActive variable
        if (switchesActive[4] && !shieldDebounce)
        {
            ShieldActivate();
            shieldDebounce = true;
        }
        else if (!switchesActive[4] && shieldDebounce)
        {
            shieldDebounce = false;
        }
    }

    public bool GetShield()
    {
        return switchesActive[4];
    }

    void ShieldActivate()
    {
        moveSpeed = switchesActive[4] ? 500f : 1000f; // Adjust speed when shield is active
    }

    void HandleBlackHole()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Mathf.Max(Vector3.Distance(transform.position, targetPosition), 0.01f); // Avoid division by zero
        float gravityForce = 2000000f / distance; // Increased pull force
        transform.position += direction * gravityForce * Time.deltaTime;
    }

    void HandleMoverInput()
    {
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerType);
        if (input == Vector2.zero)
        {
            input.x = -Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
        }

        float deskAngleRadians = Mathf.Deg2Rad * 28.81f;
        float forwardMovementZ = input.y / Mathf.Cos(deskAngleRadians);
        float upwardMovementY = input.y * Mathf.Tan(deskAngleRadians);

        Vector3 localMovement = new Vector3(forwardMovementZ, upwardMovementY, input.x) * moverSensitivity * Time.deltaTime;
        Vector3 worldMovement = spaceship.TransformDirection(localMovement);
        mover.transform.position += worldMovement;

        Vector3 localMoverPosition = spaceship.InverseTransformPoint(mover.transform.position);
        localMoverPosition = new Vector3(
            Mathf.Clamp(localMoverPosition.x, initialMoverPositionLocal.x - 0.5f, initialMoverPositionLocal.x + 0.5f),
            localMoverPosition.y,
            Mathf.Clamp(localMoverPosition.z, initialMoverPositionLocal.z - 0.5f, initialMoverPositionLocal.z + 0.5f)
        );

        mover.transform.position = spaceship.TransformPoint(localMoverPosition);
    }

    void HandleThrust()
    {
        Vector2 controllerInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerType);
        
        float thrustX = Input.GetAxis("Vertical");
        float thrustZ = Input.GetAxis("Horizontal");

        Vector3 thrustDirection = new Vector3(
            controllerInput.y + thrustX,
            0,
            controllerInput.x + thrustZ
        );

        Vector3 worldMovement = spaceship.TransformDirection(thrustDirection);
        spaceship.position += worldMovement * moveSpeed * Time.deltaTime;

        Vector3 snappedLocalPosition = Vector3.Lerp(
            spaceship.InverseTransformPoint(mover.transform.position),
            initialMoverPositionLocal,
            Time.deltaTime * (snapBackSpeed / 2)
        );

        mover.transform.position = spaceship.TransformPoint(snappedLocalPosition);
    }

    void HandleRotation()
    {
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controllerType);
        float rotY = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);

        spaceship.Rotate(rotation, Space.Self);
    }
}