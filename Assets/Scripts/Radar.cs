using UnityEngine;

public class Radar : MonoBehaviour
{
    public Transform radarSphere; // The radar sphere object
    public float radarScaleFactor = 0.01f; // Scale factor for radar objects
    public LayerMask radarLayers; // Layers to include in the radar

    [System.Obsolete]
    private void Start()
    {
        // Find all objects in the scene that are on the specified layers
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if ((radarLayers.value & (1 << obj.layer)) != 0)
            {
                CreateRadarObject(obj);
            }
        }
    }

    private void CreateRadarObject(GameObject originalObject)
    {
        // Create a new object for the radar
        GameObject radarObject = Instantiate(originalObject, radarSphere);

        // Scale down the radar object
        radarObject.transform.localScale = originalObject.transform.localScale * radarScaleFactor;

        // Adjust the position of the radar object
        radarObject.transform.position = radarSphere.position + (originalObject.transform.position - radarSphere.position) * radarScaleFactor;

        // Optionally, you can disable the mesh renderer of the radar object to make it invisible
        // radarObject.GetComponent<MeshRenderer>().enabled = false;

        // Optionally, you can add a small sphere to represent the object in the radar
        GameObject radarIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        radarIndicator.transform.SetParent(radarObject.transform);
        radarIndicator.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        radarIndicator.GetComponent<Renderer>().material.color = Color.red;
    }
}