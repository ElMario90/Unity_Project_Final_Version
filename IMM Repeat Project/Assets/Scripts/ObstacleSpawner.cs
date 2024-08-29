using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float difficultyMultiplier = 1.0f;
    public float margin = 0.5f;
    public int maxObstacles = 5;
    public int minObstacles = 1;
    public GameObject[] obstaclePrefabs;
    public GameObject[] platforms;
    public int skipPlatforms = 2; 

    private void Start()
    {
        SpawnObstaclesBasedOnDifficulty();
    }

    private void SpawnObstaclesBasedOnDifficulty()
    {
        int skipCount = 0;

        for (int i = 0; i < platforms.Length; i++)
        {
            if (skipCount < skipPlatforms)
            {
                skipCount++;
                continue; // Skip this platform
            }

            skipCount = 0;

            int obstacleCount = Mathf.CeilToInt(Random.Range(minObstacles, maxObstacles) * difficultyMultiplier);

            for (int j = 0; j < obstacleCount; j++)
            {
                GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

                Vector3 randomPosition = GetRandomPositionOnPlatform(platforms[i], obstaclePrefab);

                Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPositionOnPlatform(GameObject platform, GameObject obstaclePrefab)
    {
        var platformBounds = platform.GetComponent<Collider>().bounds;

        var obstacleCollider = obstaclePrefab.GetComponent<Collider>();

        if (obstacleCollider == null)
        {
            Debug.LogError("Obstacle prefab missing collider");
            return Vector3.zero;
        }

        var obstacleHeight = obstacleCollider.bounds.size.y;

        var randomX = Random.Range(platformBounds.min.x + margin, platformBounds.max.x - margin);
        var randomZ = Random.Range(platformBounds.min.z + margin, platformBounds.max.z - margin);

        var yPosition = platformBounds.max.y + obstacleHeight / 2 - obstacleCollider.bounds.center.y;

        return new Vector3(randomX, yPosition, randomZ);
    }
}
