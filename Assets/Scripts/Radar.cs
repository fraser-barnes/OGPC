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
        foreach (GameObject obj in radarObjects)
        {
            CreateRadarObject(obj);
        }
    }

    private void Update()
    {
        if (Time.time - lastUpdateTime > updateInterval)
        {
            UpdateRadarObjects();
            lastUpdateTime = Time.time;
        }
    }

    private void CreateRadarObject(GameObject originalObject)
    {
        MeshFilter meshFilter = originalObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = originalObject.GetComponent<MeshRenderer>();

        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogWarning("Radar object does not have a MeshFilter or MeshRenderer: " + originalObject.name);
            return;
        }

        if (!meshFilter.mesh.isReadable)
        {
            Debug.LogWarning("Mesh is not readable: " + originalObject.name);
            return;
        }

        GameObject radarObject = new GameObject(originalObject.name + "_Radar");
        radarObject.transform.SetParent(radarSphere);

        MeshFilter radarMeshFilter = radarObject.AddComponent<MeshFilter>();
        MeshRenderer radarMeshRenderer = radarObject.AddComponent<MeshRenderer>();

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

        radarMeshFilter.mesh = radarMesh;
        radarMeshRenderer.material = new Material(meshRenderer.material);

        // Position the radar object relative to the original
        radarObject.transform.position = radarSphere.position + 
            (originalObject.transform.position - radarSphere.position) * radarScaleFactor;

        radarObjectInstances.Add(radarObject);
    }

    private void UpdateRadarObjects()
    {
        foreach (GameObject radarObject in radarObjectInstances)
        {
            GameObject originalObject = radarObjects[radarObjectInstances.IndexOf(radarObject)];
            radarObject.transform.position = radarSphere.position + 
                (originalObject.transform.position - radarSphere.position) * radarScaleFactor;
        }
    }
}