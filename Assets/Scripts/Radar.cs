using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour
{
    public Transform radarSphere; // The radar sphere object
    public float radarScaleFactor = 0.01f; // Scale factor for radar objects
    public GameObject[] radarObjects; // List of objects to appear in the radar
    public float updateInterval = 0.5f; // Update interval in seconds

    private List<GameObject> radarObjectInstances = new List<GameObject>();
    private float lastUpdateTime;

    private void Start()
    {
        // Create radar objects for each specified object
        foreach (GameObject obj in radarObjects)
        {
            CreateRadarObject(obj);
        }
    }

    private void Update()
    {
        // Update radar object positions at a lower frequency
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdateRadarObjects();
            lastUpdateTime = Time.time;
        }
    }

    private void CreateRadarObject(GameObject originalObject)
    {
        // Get the mesh filter and renderer from the original object
        MeshFilter meshFilter = originalObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = originalObject.GetComponent<MeshRenderer>();

        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogWarning("Radar object does not have a MeshFilter or MeshRenderer: " + originalObject.name);
            return;
        }

        // Create a new GameObject for the radar
        GameObject radarObject = new GameObject(originalObject.name + "_Radar");
        radarObject.transform.SetParent(radarSphere);

        // Create a new mesh filter and renderer for the radar object
        MeshFilter radarMeshFilter = radarObject.AddComponent<MeshFilter>();
        MeshRenderer radarMeshRenderer = radarObject.AddComponent<MeshRenderer>();

        // Create a readable copy of the mesh
        Mesh originalMesh = meshFilter.mesh;
        Mesh radarMesh = new Mesh();
        radarMesh.vertices = originalMesh.vertices;
        radarMesh.triangles = originalMesh.triangles;
        radarMesh.normals = originalMesh.normals;
        radarMesh.uv = originalMesh.uv;
        radarMesh.RecalculateBounds();

        // Scale the vertices
        Vector3[] scaledVertices = radarMesh.vertices;
        for (int i = 0; i < scaledVertices.Length; i++)
        {
            scaledVertices[i] *= radarScaleFactor;
        }
        radarMesh.vertices = scaledVertices;
        radarMesh.RecalculateBounds();

        // Assign the scaled mesh to the radar object
        radarMeshFilter.mesh = radarMesh;

        // Copy the material from the original object
        radarMeshRenderer.material = new Material(meshRenderer.material);

        // Adjust the position of the radar object
        radarObject.transform.position = radarSphere.position + (originalObject.transform.position - radarSphere.position) * radarScaleFactor;

        // Add the radar object to the list
        radarObjectInstances.Add(radarObject);
    }

    private void UpdateRadarObjects()
    {
        // Update the positions of all radar objects
        foreach (GameObject radarObject in radarObjectInstances)
        {
            GameObject originalObject = radarObjects[radarObjectInstances.IndexOf(radarObject)];
            radarObject.transform.position = radarSphere.position + (originalObject.transform.position - radarSphere.position) * radarScaleFactor;
        }
    }
}