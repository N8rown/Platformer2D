using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSpawner : MonoBehaviour
{

    public GameObject pointPrefab;
    public float spawnInterval = 5f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        GameObject duplicate = Instantiate(pointPrefab);
        InvokeRepeating("SpawnPoint", 2f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
