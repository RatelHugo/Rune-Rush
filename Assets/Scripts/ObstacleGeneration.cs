using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] obstaclePrefabs;  
    public Transform player;              
    public float obstacleInterval = 25f;  

    private float nextObstacleZ = 0f;
    private float ObstacleY = 2.7f;

    void Start()
    {
        nextObstacleZ = player.position.z + 20f;
    }

    void Update()
    {
        if (player.position.z + 50f > nextObstacleZ)
        {
            SpawnObstacle();
            nextObstacleZ += obstacleInterval;
        }
    }

    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstaclePrefab = obstaclePrefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(0f, ObstacleY, nextObstacleZ);
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
