using UnityEngine;

public class SwitchDetectorScript : MonoBehaviour
{
    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if the ray hits a collider
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the hit object has a collider (optional, but good practice)
                if (hit.collider != null)
                {
                    // Perform actions based on the clicked object
                    Debug.Log("Clicked object: " + hit.collider.gameObject.name);
                    // Example:  You can access the clicked object here: hit.collider.gameObject
                }
            }
        }
    }
}
