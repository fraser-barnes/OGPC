using UnityEngine;

public class arrowPointer : MonoBehaviour
{
    public Transform target; // Assign this in the Inspector or dynamically during runtime

    void Update()
    {
        if (target != null)
        {
            // Calculate direction to the target
            Vector3 direction = target.position - transform.position;

            // Update the arrow's rotation to face the target
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    // Optional: Set a new target dynamically
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
