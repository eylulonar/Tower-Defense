using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform monster;
    //public Transform monster2;
    public Transform startPoint;
    public float timebetweenWaves;
    private float countdown = 6f;
    private int waveIndex =0;

    //As the game proceeds, to increase the difficulty, the count of spawned monsters is increased by 1 at each subsequent waveIndex.

    ////Second monster is spawned after 7 seconds from start and will be kept spawning after each 5 seconds.
    //private void Start()
    //{
    //    InvokeRepeating(nameof(SpawnSecondMonster),12f, 10f);
        
    //}

    //First spawn starts after 6 seconds and time between each wave is set to 3 seconds in the inspector. When game is over, it stops spawning new monsters.
    private void Update()
    {
        if (PlayerPrefs.HasKey("isGameEnded"))
        {
            string result = PlayerPrefs.GetString("isGameEnded");
            if (result == "Yes")
            {
                enabled = false;

            }
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timebetweenWaves;
        }

        countdown -= Time.deltaTime;

    }

    //At one particular wave index, the spawn time between each monster is set to 2 times per second with coroutine.
    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i=0; i< waveIndex; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void SpawnMonster()
    {
       
        Instantiate(monster, startPoint.position, startPoint.rotation);
    }

    //void SpawnSecondMonster()
    //{
    //   
    //    Instantiate(monster2, startPoint.position, startPoint.rotation);
       
    //}
 
}
