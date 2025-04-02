using UnityEngine;

public class BlasterMovement : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    //private Rigidbody rb;
    public Transform objectB; // The object whose rotation we want to track
    private Quaternion initialRotationB;

    void Start()
    {
        initialRotationB = objectB.rotation;
        transform.Rotate(0, 0, 0); // Rotate the bullet 90 degrees towards the X-axis initially
    }


    void Update()
    {
        // Continuously apply velocity to maintain speed along the direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Quaternion rotationDelta = objectB.rotation * Quaternion.Inverse(initialRotationB);

       // Apply the rotation change to this object
       transform.rotation = rotationDelta * transform.rotation;
    }
}
