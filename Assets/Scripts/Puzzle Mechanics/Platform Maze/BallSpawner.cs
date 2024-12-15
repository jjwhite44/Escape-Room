using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spherePrefab; // Prefab of the sphere to be spawned

    [SerializeField]
    private Transform spawnPoint; // The initial spawn point of the sphere

    private GameObject currentSphere;

    [SerializeField]
    private float fallThreshold = -10.0f; // The Y-coordinate threshold below which the sphere is considered to have fallen

    void Start()
    {
        // Spawn the initial sphere
        SpawnSphere();
    }

    private void Update()
    {
        // Check if the sphere has fallen below the threshold
        if (currentSphere != null && currentSphere.transform.position.y < fallThreshold)
        {
            // Sphere fell below the threshold, spawn a new one
            SpawnSphere();
        }
    }

    void SpawnSphere()
    {
        // Destroy the current sphere if it exists
        if (currentSphere != null)
        {
            Destroy(currentSphere);
        }

        // Instantiate a new sphere at the spawn point
        currentSphere = Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);
    }
}
