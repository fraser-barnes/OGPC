using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public string tagToCheck = "MyTag"; // Set this in the Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag(tagToCheck))
                {
                    Debug.Log("Clicked object with tag: " + tagToCheck);
                    // Add your code here to handle the click on the object with the tag
                }
            }
        }
    }

}