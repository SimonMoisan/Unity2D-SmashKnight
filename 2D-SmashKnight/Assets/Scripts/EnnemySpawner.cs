using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public float timeOffset;
    private float timeBtwSpawn;
    public float startTimeBtwSpawns;
    public int numberOfEnnemies;
    public int numberOfEnnemiesSpawned = 0;

    public GameObject ennemyPrefab;
    public string spawnOrientation;

    public bool isActive = false;
    public string layer;
    public int verticalPosition;
    public string horizontalPosition;

    public EnnemyManager ennemyManager;

    private void Start()
    {
        timeBtwSpawn = timeOffset;
        startTimeBtwSpawns += Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ennemyManager = FindObjectOfType<EnnemyManager>();
        SpawnWaveOfEnnemies();
    }

    private void SpawnWaveOfEnnemies()
    {
        if (timeBtwSpawn <= 0 && numberOfEnnemiesSpawned < numberOfEnnemies)
        {
            GameObject ennemyGO = Instantiate(ennemyPrefab, transform.position, Quaternion.identity);
            ennemyManager.totalToEnnemySpawn--;

            ennemyGO.GetComponent<Ennemy>().setLayer(layer, verticalPosition);

            if (spawnOrientation == "Right")
            {
                ennemyGO.GetComponent<Ennemy>().ChangeOrientation("Right");
            }
            else if (spawnOrientation == "Left")
            {
                ennemyGO.GetComponent<Ennemy>().ChangeOrientation("Left");
            }

            numberOfEnnemiesSpawned++;
            timeBtwSpawn = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
