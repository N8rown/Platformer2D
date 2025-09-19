using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSpawner : MonoBehaviour
{

    public GameObject pointPrefab;
    public float spawnInterval = 5f;
    private float timer;
    public float xLeft = -18f;
    public float xRight = 45f;
    void SpawnPoint()
    {
        float randomX = Random.Range(xLeft, xRight);
        Vector3 spawnPosition = new Vector3(randomX, 30f, 0f);
        Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPoint();
        }
    }
    void Start()
    {
        SpawnPoint();
        StartCoroutine(SpawnRoutine());
    }

}
