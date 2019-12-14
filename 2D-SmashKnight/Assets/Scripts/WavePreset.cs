using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WavePreset : ScriptableObject
{
    public string waveName;

    //Wave caracteristics
    [Header("Spawner parameters :")]
    [Header("order : 0:L1, 1:L2, 2:L3, 3:R1, 4:R2, 5:R3")] 
    public bool[] activeSpawners;
    public int[] numberOfEnnemies;
    public int[] timeOffsets;
    public float[] timeBtwSpawn;
    public Ennemy[] ennemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
