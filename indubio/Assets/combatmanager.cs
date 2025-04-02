using System.Collections.Generic;
using UnityEngine;

public class combatmanager : MonoBehaviour
{
    public static int enemyCount = 0;
    int enemyIndex = 0;
    public GameObject[] enemyTypes; //0 = test, 1 = nerv, 2 = scare, 3= anger\
    public Vector2[] WavePositions;
    public int[] enemiesInWave;
    public int[] numbersInWaves;
    int wave = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void spawn()
    {
        for(int i = 0; i < numbersInWaves[wave]; i++)
        {
            enemyCount++;
            var tmpEnemy = Instantiate(enemyTypes[enemiesInWave[enemyIndex]], WavePositions[enemyIndex], Quaternion.identity);
            enemyIndex++;
       
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyCount == 0)
        {
            if(wave<numbersInWaves.Length)
            {
                spawn();
            }           
            wave++;

        }
    }
}
