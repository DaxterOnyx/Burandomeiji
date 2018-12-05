using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemyScript : MonoBehaviour {

    public int numWave = 0;
    public bool waveFinnish = true;

    public float timeSpawn = 0.5f;
    public float timeRespawn = 0;

    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    }

    [System.Serializable]
    public class Wave
    {
        public WaveComponent[] waveComponent;
    }
    public Wave[] waves;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeRespawn -= Time.deltaTime;
        
        if (timeRespawn < 0 && numWave <= waves.Length && !waveFinnish)
        {
            timeRespawn = timeSpawn;

            waveFinnish = true;

            // Go through the wave comps until we find something to spawn;
            foreach (WaveComponent wc in waves[numWave-1].waveComponent)
            {
                if (wc.spawned < wc.num)
                {
                    // Spawn it
                    wc.spawned++;
                    Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);

                    waveFinnish=false ;
                    break;
                }
            }
        }
    }

    public void NextWave()
    {
        Debug.Log(numWave);
        //Debug.Log(timeRespawn < 0 && numWave < waves.Length && !waveFinnish);
        numWave =numWave+1;
        waveFinnish = false;
    }
}
