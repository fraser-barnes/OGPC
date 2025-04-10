using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    //public string tagToCheck = "MyTag"; // Set this in the Inspector
    private bool shieldsActive = false;
    private bool radarActive = false;
    private bool courseHelperActive = false;
    private bool blastersActive = false;
    private bool heatingActive = false;
    private bool steeringActive = false;
    private bool repairActive = false;
    private bool flashlightActive = false;
    private bool thrustActive = false;
    private bool windowWipersActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                Debug.Log("Clicked object: " + hit.transform.gameObject.name);

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

                if (hit.transform.gameObject.CompareTag("HeatingSwitch") && !heatingActive)
                {
                    heatingActive = true;
                    Debug.Log("Clicked object with tag: HeatingSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("HeatingSwitch") && heatingActive)
                {
                    heatingActive = false;
                    Debug.Log("Clicked object with tag: HeatingSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("SteeringSwitch") && !steeringActive)
                {
                    steeringActive = true;
                    Debug.Log("Clicked object with tag: SteeringSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("SteeringSwitch") && steeringActive)
                {
                    steeringActive = false;
                    Debug.Log("Clicked object with tag: SteeringSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("RepairSwitch") && !repairActive)
                {
                    repairActive = true;
                    Debug.Log("Clicked object with tag: RepairSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("RepairSwitch") && repairActive)
                {
                    repairActive = false;
                    Debug.Log("Clicked object with tag: RepairSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("FlashlightSwitch") && !flashlightActive)
                {
                    flashlightActive = true;
                    Debug.Log("Clicked object with tag: FlashlightSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("FlashlightSwitch") && flashlightActive)
                {
                    flashlightActive = false;
                    Debug.Log("Clicked object with tag: FlashlightSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("ThrustSwitch") && !thrustActive)
                {
                    thrustActive = true;
                    Debug.Log("Clicked object with tag: ThrustSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("ThrustSwitch") && thrustActive)
                {
                    thrustActive = false;
                    Debug.Log("Clicked object with tag: ThrustActiveSwitch Off");
                }

                if (hit.transform.gameObject.CompareTag("WindowWipersSwitch") && !windowWipersActive)
                {
                    windowWipersActive = true;
                    Debug.Log("Clicked object with tag: WindowWipersSwitch On");
                    // Add your code here to handle the click on the object with the tag
                }
                else if (hit.transform.gameObject.CompareTag("WindowWipersSwitch") && windowWipersActive)
                {
                    windowWipersActive = false;
                    Debug.Log("Clicked object with tag: WindowWipersSwitch Off");
                }
            }
        }
    }

}