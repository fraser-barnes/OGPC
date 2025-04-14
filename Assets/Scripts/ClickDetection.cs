using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public bool[] switchesActive = new bool[10];
    // left to right, going down the rows
    // 0 lighting, 1 steering, 2 blasters, 3 thrust, 4 shields, 5 course helper, 6 repair, 7 radar, 8 window wipers, 9 heating

    [SerializeField]
    private GameObject[] switches;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {

                Debug.Log("Clicked object: " + hit.transform.gameObject.name);

                if (hit.collider.gameObject.CompareTag("ShieldsSwitch"))
                {
                    switchesActive[4] = !switchesActive[4];
                }

                else if (hit.collider.gameObject.CompareTag("RadarSwitch"))
                {
                    switchesActive[7] = !switchesActive[7];
                }

                else if (hit.collider.gameObject.CompareTag("CourseHelperSwitch"))
                {
                    switchesActive[5] = !switchesActive[5];
                }

                else if (hit.collider.gameObject.CompareTag("BlastersSwitch"))
                {
                    switchesActive[2] = !switchesActive[2];
                }

                else if (hit.collider.gameObject.CompareTag("HeatingSwitch"))
                {
                    switchesActive[9] = !switchesActive[9];
                }

                else if (hit.collider.gameObject.CompareTag("SteeringSwitch"))
                {
                    switchesActive[1] = !switchesActive[1];
                }

                else if (hit.collider.gameObject.CompareTag("RepairSwitch"))
                {
                    switchesActive[6] = !switchesActive[6];
                }

                else if (hit.collider.gameObject.CompareTag("LightSwitch"))
                {
                    switchesActive[0] = !switchesActive[0];
                }

                else if (hit.collider.gameObject.CompareTag("ThrustSwitch"))
                {
                    switchesActive[3] = !switchesActive[3];
                }

                else if (hit.collider.gameObject.CompareTag("WindowWipersSwitch"))
                {
                    switchesActive[8] = !switchesActive[8];
                }
            }
        }
    }

}