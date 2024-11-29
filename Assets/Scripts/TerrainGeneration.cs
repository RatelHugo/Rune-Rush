using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [Header("Settings")]
    public GameObject terrainPrefab;  
    public Transform player;          
    public int maxTerrains = 2;       
    public float terrainLength = 51f; 
    public float spawnY = 5.8f;       

    private List<GameObject> terrains = new List<GameObject>(); 
    private float nextSpawnZ = 19f;

    void Start()
    {
        for (int i = 0; i < maxTerrains; i++)
        {
            SpawnTerrain();
        }
    }

    void Update()
    {    
        if (player.position.z > terrains[0].transform.position.z + 35)
        {
            DeleteAndSpawnNewTerrain();
        }
    }

    void DeleteAndSpawnNewTerrain()
    {
        GameObject oldTerrain = terrains[0];
        terrains.RemoveAt(0);
        Destroy(oldTerrain);

        SpawnTerrain();
    }

    void SpawnTerrain()
    {
        GameObject newTerrain = Instantiate(terrainPrefab);

        newTerrain.transform.position = new Vector3(0, spawnY, nextSpawnZ);
        nextSpawnZ += terrainLength;

        terrains.Add(newTerrain);
    }
}
