using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorController : MonoBehaviour
{
    public GameObject[] roads;
    private List<GameObject> usedRoads = new List<GameObject>();
    private float spawnPos = 0;
    private float roadLength = 100;

    [SerializeField] private Transform player;
    private int startRoads = 5;

    private void Start()
    {
        SpawnRoad(0);

        for (int i = 0; i < startRoads; i++)
            SpawnRoad(Random.Range(1, roads.Length));
    }

    private void Update()
    {
        if (player.position.z - 60 > spawnPos - (startRoads * roadLength)){
            SpawnRoad(Random.Range(1, roads.Length));
            DeleteRoad();
        }
    }

    private void SpawnRoad(int index)
    {
        GameObject nextRoad = Instantiate(roads[index], transform.forward * spawnPos, transform.rotation, transform);
        nextRoad.transform.localPosition = transform.forward * spawnPos;
        usedRoads.Add(nextRoad);
        spawnPos += roadLength;
    }

    private void DeleteRoad()
    {
        Destroy(usedRoads[0]);
        usedRoads.RemoveAt(0);
    }
}
