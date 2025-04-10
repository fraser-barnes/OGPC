using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public string tagToCheck = "MyTag"; // Set this in the Inspector
    public bool shieldsActive = false;
    public bool radarActive = false;
    public bool courseHelperActive = false;
    public bool blastersActive = false;

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

                if (hit.transform.gameObject.CompareTag("RadarSwitch") && !radarActive)
                {
                    radarActive = true;
                    Debug.Log("Clicked object with tag: RadarSwitch");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("RadarSwitch") && radarActive)
                {
                    radarActive = false;
                    Debug.Log("Clicked object with tag: RadarSwitch");
                }

                if (hit.transform.gameObject.CompareTag("CourseHelperSwitch") && !courseHelperActive)
                {
                    courseHelperActive = true;
                    Debug.Log("Clicked object with tag: CourseHelperSwitch");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("CourseHelperSwitch") && courseHelperActive)
                {
                    courseHelperActive = false;
                    Debug.Log("Clicked object with tag: CourseHelperSwitch");
                }

                if (hit.transform.gameObject.CompareTag("BlastersSwitch") && !blastersActive)
                {
                    blastersActive = true;
                    Debug.Log("Clicked object with tag: BlastersSwitch");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("BlastersSwitch") && blastersActive)
                {
                    blastersActive = false;
                    Debug.Log("Clicked object with tag: BlastersSwitch");
                }
            }
        }
    }

}