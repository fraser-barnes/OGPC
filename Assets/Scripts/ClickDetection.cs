using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public bool[] switchesActive = new bool[10];
    // left to right, going down the rows
    // 0 lighting, 1 steering, 2 blasters, 3 thrust, 4 shields, 5 course helper, 6 repair, 7 radar, 8 window wipers, 9 heating

    [SerializeField]
    private GameObject[] switches;
    private int i;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {

                if (hit.collider.gameObject.CompareTag("ShieldsSwitch"))
                {
                    i = 4;
                }
                else if (hit.collider.gameObject.CompareTag("RadarSwitch"))
                {
                    i = 7;
                }
                else if (hit.collider.gameObject.CompareTag("CourseHelperSwitch"))
                {
                    i = 5;
                }
                else if (hit.collider.gameObject.CompareTag("BlastersSwitch"))
                {
                    i = 2;
                }
                else if (hit.collider.gameObject.CompareTag("HeatingSwitch"))
                {
                    i = 9;
                }
                else if (hit.collider.gameObject.CompareTag("SteeringSwitch"))
                {
                    i = 1;
                }
                else if (hit.collider.gameObject.CompareTag("RepairSwitch"))
                {
                    i = 6;
                }
                else if (hit.collider.gameObject.CompareTag("LightSwitch"))
                {
                    i = 0;
                }
                else if (hit.collider.gameObject.CompareTag("ThrustSwitch"))
                {
                    i = 3;
                }
                else if (hit.collider.gameObject.CompareTag("WindowWipersSwitch"))
                {
                    i = 8;
                }
                switchesActive[i] = !switchesActive[i];
                switches[i].transform.localScale = new Vector3(110f, switches[i].transform.localScale.y * -1f, 110f);
                switches[i].transform.rotation = Quaternion.Euler(0, switches[i].transform.rotation.y + 180f, 0);
            }
        }
    }

}