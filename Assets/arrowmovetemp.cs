using UnityEngine;

public class arrowmovetemp : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the cube moves

    // Update is called once per frame
    void Update()
    {
        // Get input from arrow keys (or WASD as an alternative)
        float moveHorizontal = Input.GetAxis("Horizontal"); // Left and Right arrow keys
        float moveVertical = Input.GetAxis("Vertical"); // Up and Down arrow keys

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Move the cube
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
