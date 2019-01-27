using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;

    [SerializeField]
    private CompositeCollider2D[] spawnAreas;

    [SerializeField]
    private int numToSpawn = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Cannot spawn a nonexistant object!");
            return;
        }
        if (spawnAreas.Length == 0)
        {
            Debug.LogError("Cannot spawn an object in no areas!");
            return;
        }

        Renderer objectRenderer = objectToSpawn.GetComponent<Renderer>();
        Vector2 extents = objectRenderer != null ? new Vector2(objectRenderer.bounds.extents.x, objectRenderer.bounds.extents.y) : Vector2.zero;
        extents = objectToSpawn.transform.TransformVector(extents);

        int numSpawned = 0;
        int i = 0;
        while (numSpawned < numToSpawn)
        {
            CompositeCollider2D spawnArea = spawnAreas[Mathf.FloorToInt(spawnAreas.Length * Random.Range(0, .9999999f))];
            CompositeCollider2D.GeometryType geoType = spawnArea.geometryType;
            spawnArea.geometryType = CompositeCollider2D.GeometryType.Polygons;

            Bounds spawnBounds = spawnArea.bounds;
            Vector2 spawnExtents = spawnBounds.extents;
            spawnExtents -= extents;

            Vector2 point = spawnArea.offset + new Vector2(spawnBounds.center.x, spawnBounds.center.y) + new Vector2(
                Random.Range(-spawnBounds.extents.x, spawnBounds.extents.x),
                Random.Range(-spawnBounds.extents.y, spawnBounds.extents.y)
                ) * .9f;

            if (spawnArea.OverlapPoint(point))
            {
                GameObject go = Instantiate(objectToSpawn, transform);
                go.transform.position = new Vector3(point.x, point.y, 0f);
                go.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                ++numSpawned;
            }

            spawnArea.geometryType = geoType;
            ++i;
            if (i > 100000)
            {
                Debug.LogError("Over 100000 iterations for spawning! Something must be wrong...");
                return;
            }
        }
    }
}
