using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    //public string tagToCheck = "MyTag"; // Set this in the Inspector
    public bool shieldsActive = false;
    public bool radarActive = false;
    public bool courseHelperActive = false;
    public bool blastersActive = false;
    public bool coolingActive = false;
    public bool stearingActive = false;

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
                    Debug.Log("Clicked object with tag: ShieldsSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("ShieldsSwitch") && shieldsActive)
                {
                    shieldsActive = false;
                    Debug.Log("Clicked object with tag: ShieldsSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("RadarSwitch") && !radarActive)
                {
                    radarActive = true;
                    Debug.Log("Clicked object with tag: RadarSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("RadarSwitch") && radarActive)
                {
                    radarActive = false;
                    Debug.Log("Clicked object with tag: RadarSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("CourseHelperSwitch") && !courseHelperActive)
                {
                    courseHelperActive = true;
                    Debug.Log("Clicked object with tag: CourseHelperSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("CourseHelperSwitch") && courseHelperActive)
                {
                    courseHelperActive = false;
                    Debug.Log("Clicked object with tag: CourseHelperSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("BlastersSwitch") && !blastersActive)
                {
                    blastersActive = true;
                    Debug.Log("Clicked object with tag: BlastersSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("BlastersSwitch") && blastersActive)
                {
                    blastersActive = false;
                    Debug.Log("Clicked object with tag: BlastersSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("CoolingSwitch") && !coolingActive)
                {
                    blastersActive = true;
                    Debug.Log("Clicked object with tag: CoolingSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("CoolingSwitch") && coolingActive)
                {
                    blastersActive = false;
                    Debug.Log("Clicked object with tag: CoolingSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("StearingSwitch") && !stearingActive)
                {
                    blastersActive = true;
                    Debug.Log("Clicked object with tag: StearingSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("StearingSwitch") && stearingActive)
                {
                    blastersActive = false;
                    Debug.Log("Clicked object with tag: StearingSwitch Off");
                }
            }
        }
    }

}