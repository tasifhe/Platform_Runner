using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Array of prefabs to spawn
    public int spawnCount = 5; // The number of clones to create
    public float spawnDistance = 500f; // The distance between each clone
    public PlayerController playerController; // Reference to the PlayerController script
    public float activationDistance = 500f; // Distance at which the objects should be activated

    private GameObject[] spawnedObjects; // Array to store the spawned objects
    private int currentIndex = 0; // Current index in the spawnedObjects array

    private void Start()
    {
        spawnedObjects = new GameObject[spawnCount]; // Initialize the array
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int prefabIndex = Random.Range(0, prefabsToSpawn.Length);
            GameObject prefab = prefabsToSpawn[prefabIndex];

            Vector3 spawnPosition = transform.position + transform.forward * (i * spawnDistance);

            // Spawn the object
            spawnedObjects[i] = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Parent the spawned object to this SpawnManager
            spawnedObjects[i].transform.parent = transform;

            // Disable the spawned object initially
            spawnedObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        if (playerController != null)
        {
            Transform playerTransform = playerController.transform;

            // Check the distance between the player and the current spawned object
            if (currentIndex < spawnedObjects.Length)
            {
                if (Vector3.Distance(playerTransform.position, spawnedObjects[currentIndex].transform.position) <= activationDistance)
                {
                    // Enable the current spawned object if it is within the activation distance
                    spawnedObjects[currentIndex].SetActive(true);
                    currentIndex++;
                }
            }
            else
            {
                currentIndex = 0; // Reset the index if we've reached the end of the spawnedObjects array
            }
        }
    }
}
