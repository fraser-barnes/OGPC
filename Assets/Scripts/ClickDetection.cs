using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public string tagToCheck = "MyTag"; // Set this in the Inspector
    public bool shieldsActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.CompareTag("ShieldsSwitch") && !shieldsActive)
                {
                    shieldsActive = true;
                    Debug.Log("Clicked object with tag: ShieldsSwitch");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("ShieldsSwitch") && shieldsActive)
                {
                    shieldsActive = false;
                    Debug.Log("Clicked object with tag: ShieldsSwitch");
                }
            }
        }
    }

}