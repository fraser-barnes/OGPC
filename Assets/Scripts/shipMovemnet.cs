using UnityEditor;
using UnityEngine;

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
    public bool shieldActive;

    private bool shieldDebounce = false;
    private Vector3 initialMoverPositionLocal; // Mover position relative to the spaceship

    void Start()
    {
        if (mover == null || spaceship == null)
        {
            Debug.LogError("Please assign the mover and spaceship objects.");
            enabled = false;
            return;
        }

        // Spawn the black hole
        negativeSign = Random.Range(1, 3);
        if (negativeSign == 1)
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
    }

    void Update()
    {
        HandleMoverInput();
        HandleThrust();
        HandleRotation();
        HandleBlackHole();

        if (Input.GetKey(KeyCode.K) && !shieldDebounce)
        {
            ShieldActivate();
            shieldDebounce = true;
        }
        else if (!Input.GetKey(KeyCode.K) && shieldDebounce)
        {
            shieldDebounce = false;
        }
    }

    public bool GetShield()
    {
        return shieldActive;
    }

    void ShieldActivate()
    {
        shieldActive = !shieldActive;
        moveSpeed = shieldActive ? 500f : 1000f; // Adjust speed when shield is active
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
        Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
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
        Vector3 localOffset = spaceship.InverseTransformPoint(mover.transform.position) - initialMoverPositionLocal;
        float thrustX = 0f, thrustZ = 0f;

        if (Input.GetKey(KeyCode.W)) thrustX = 1f;
        if (Input.GetKey(KeyCode.S)) thrustX = -1f;
        if (Input.GetKey(KeyCode.A)) thrustZ = 1f;
        if (Input.GetKey(KeyCode.D)) thrustZ = -1f;

        Vector3 thrustDirection = new Vector3(localOffset.x + thrustX, 0, localOffset.z + thrustZ);
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
        Vector2 rotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        Vector3 rotation = new Vector3(0, rotationInput.x * rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKey(KeyCode.DownArrow)) rotation.z -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow)) rotation.z += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow)) rotation.y -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) rotation.y += rotationSpeed * Time.deltaTime;

        spaceship.Rotate(rotation, Space.Self);
    }
}
