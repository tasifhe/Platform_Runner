using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public int initialSpawnCount = 5; // The number of initial clones to create
    public float spawnDistance = 500f; // The distance between each clone
    public float activationDistance = 500f; // Distance at which the objects should be activated

    private PlayerController playerController; // Reference to the PlayerController instance
    public Transform playerTransform; // Reference to the player's transform

    private List<GameObject> spawnedObjects; // List to store the spawned objects
    private int currentIndex = 0; // Index of the current object

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find the PlayerController instance in the scene
        spawnedObjects = new List<GameObject>(); // Initialize the list
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < initialSpawnCount; i++)
        {
            Vector3 spawnPosition = transform.position + transform.forward * (i * spawnDistance);

            // Spawn the object
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Parent the spawned object to this SpawnManager
            spawnedObject.transform.parent = transform;

            // Disable the spawned object initially
            spawnedObject.SetActive(false);

            // Add the spawned object to the list
            spawnedObjects.Add(spawnedObject);
        }
    }

    private void Update()
    {
        if (playerController != null && playerTransform == null)
        {
            playerTransform = playerController.transform;
        }

        // Check if the player is close to the current spawned object
        if (currentIndex < spawnedObjects.Count && playerTransform != null && Vector3.Distance(playerTransform.position, spawnedObjects[currentIndex].transform.position) <= activationDistance)
        {
            // Enable the current spawned object if it is within the activation distance
            spawnedObjects[currentIndex].SetActive(true);
            currentIndex++;
        }

        // Check if all objects have been activated
        if (currentIndex >= spawnedObjects.Count)
        {
            // Generate a new object and add it to the list
            Vector3 spawnPosition = spawnedObjects[spawnedObjects.Count - 1].transform.position + transform.forward * spawnDistance;
            GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            newObject.transform.parent = transform;
            spawnedObjects.Add(newObject);

            // Disable the new object initially
            newObject.SetActive(false);

            currentIndex = spawnedObjects.Count - 1;
        }
    }
}
