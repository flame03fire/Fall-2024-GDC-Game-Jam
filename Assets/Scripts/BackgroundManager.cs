using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] prefabList;
    public float minSpeed = 1.0f;
    public float maxSpeed = 5.0f;
    public float minSpawnX = -5.0f;
    public float maxSpawnX = 5.0f;
    public float spawnY = 10.0f; // New variable for spawn Y location
    public float spawnInterval = 1.0f;
    public float despawnY = -10.0f;

    private float spawnTimer = 0.0f;

    void Update()
    {
        // Update spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new prefab
        if (spawnTimer >= spawnInterval)
        {
            // Reset spawn timer
            spawnTimer = 0.0f;

            // Instantiate a random prefab
            GameObject prefab = prefabList[Random.Range(0, prefabList.Length)];

            // Random position along x-axis
            float spawnX = Random.Range(minSpawnX, maxSpawnX);

            // Instantiate the prefab at the random position with the specified spawn Y location
            GameObject newPrefab = Instantiate(prefab, new Vector3(spawnX, spawnY, 0), Quaternion.identity);

            // Random speed for scrolling
            float speed = Random.Range(minSpeed, maxSpeed);

            // Assign random speed to the prefab's scrolling script
            BackgroundScrolling scrollingScript = newPrefab.GetComponent<BackgroundScrolling>();
            if (scrollingScript != null)
            {
                scrollingScript.scrollSpeed = speed;
            }
        }

        // Check for despawning
        foreach (Transform child in transform)
        {
            if (child.position.y <= despawnY)
            {
                Destroy(child.gameObject);
            }
        }
    }
}


