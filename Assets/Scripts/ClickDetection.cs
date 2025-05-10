using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public bool[] switchesActive = new bool[10];
    // left to right, going down the rows
    // 0 lighting, 1 steering, 2 blasters, 3 thrust, 4 shields, 5 course helper, 6 repair, 7 radar, 8 window wipers, 9 heating

    [SerializeField]
    private GameObject[] switches;
    private int i;

    [SerializeField]
    private Transform rightHandAnchor;
    [SerializeField]
    private LineRenderer rayRenderer;
    [SerializeField]
    private GameObject hitMarker;
    [SerializeField]
    private LayerMask switchLayer;

    private OVRInput.Controller controllerR = OVRInput.Controller.RTouch;

    void Update()
    {
        Vector3 rayOrigin = rightHandAnchor.position;
        Vector3 rayDirection = rightHandAnchor.forward;

        RaycastHit hit;
        rayRenderer.SetPosition(0, rayOrigin);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 900f, switchLayer))
        {
            Debug.Log("Hit: " + hit.collider.name);
            rayRenderer.SetPosition(1, hit.point);

            if (hitMarker != null)
            {
                hitMarker.transform.position = hit.point;

                Renderer markerRenderer = hitMarker.GetComponent<Renderer>();
                if (markerRenderer != null)
                {
                    markerRenderer.material.color = Color.green;
                }

                hitMarker.transform.localScale = Vector3.one * 5f;
                hitMarker.SetActive(true);
            }
            else
            {
                Debug.LogWarning("HitMarker GameObject not assigned.");
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controllerR))
            {
                GameObject obj = hit.collider.gameObject;
                string tag = obj.tag;

                if (tag == "ShieldsSwitch") i = 4;
                else if (tag == "RadarSwitch") i = 7;
                else if (tag == "CourseHelperSwitch") i = 5;
                else if (tag == "BlastersSwitch") i = 2;
                else if (tag == "HeatingSwitch") i = 9;
                else if (tag == "SteeringSwitch") i = 1;
                else if (tag == "RepairSwitch") i = 6;
                else if (tag == "FlashlightSwitch") i = 0;
                else if (tag == "ThrustSwitch") i = 3;
                else if (tag == "WindowWipersSwitch") i = 8;
                else return;

                switchesActive[i] = !switchesActive[i];
                switches[i].SetActive(!switches[i].activeSelf);
                switches[i + 10].SetActive(!switches[i + 10].activeSelf);
            }
        }
        else
        {
            rayRenderer.SetPosition(1, rayOrigin + rayDirection * 100f);

            if (hitMarker != null)
            {
                hitMarker.transform.position = rayOrigin + rayDirection * 50f;

                Renderer markerRenderer = hitMarker.GetComponent<Renderer>();
                if (markerRenderer != null)
                {
                    markerRenderer.material.color = Color.red;
                }

                hitMarker.transform.localScale = Vector3.one * 3f;
                hitMarker.SetActive(true);
            }
            else
            {
                Debug.LogWarning("HitMarker GameObject not assigned.");
            }
        }
    }

}