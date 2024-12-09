using UnityEngine;

public class arrowPointer : MonoBehaviour
{
    public Transform target; // Assign in the Inspector or dynamically during runtime

    void Update()
{
    if (target != null)
    {
        // Calculate direction to the target
        Vector3 direction = target.position - transform.position;

        // Calculate the rotation and apply an offset to account for the arrow's default orientation
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation * Quaternion.Euler(90, 0, 0); // Rotate 90° around the X-axis
    }
}

    // Optional: Set a new target dynamically
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
