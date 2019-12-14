using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour
{
    public EnnemySpawner[] ennemySpawners;
    public Transform[] ennemyDestroyers;
    public PlayerMovement player;
    
    public int totalToEnnemySpawn;
    public int ennemiesReachedLeftDestroyer;
    public int ennemiesReachedRightDestroyer;

    public WavePreset wavepreset;

    // Start is called before the first frame update
    void Start()
    {
        if(wavepreset != null)
        {
            for(int i=0; i<ennemySpawners.Length; i++)
            {
                if(wavepreset.activeSpawners[i])
                {

                }
                else
                {

                }
            }
        }

        player = FindObjectOfType<PlayerMovement>();
        ennemySpawners = FindObjectsOfType<EnnemySpawner>();
        for (int i = 0; i < ennemySpawners.Length; i++)
        {
            totalToEnnemySpawn += ennemySpawners[i].numberOfEnnemies;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ennemiesReachedLeftDestroyer > 0)
        {
            AddEnnemyOnSpawner("Left", player.verticalPosition);
            ennemiesReachedLeftDestroyer--;
        }

        if(ennemiesReachedRightDestroyer > 0)
        {
            AddEnnemyOnSpawner("Right", player.verticalPosition);
            ennemiesReachedRightDestroyer--;
        }
    }

    public void AddEnnemyOnSpawner(string side, int verticalPosition)
    {
        for (int i = 0; i < ennemySpawners.Length; i++)
        {
            if(ennemySpawners[i].verticalPosition == verticalPosition && side == ennemySpawners[i].horizontalPosition)
            {
                ennemySpawners[i].numberOfEnnemiesSpawned--;
            }
        }
    }
}
