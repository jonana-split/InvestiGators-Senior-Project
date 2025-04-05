using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class combatmanager : MonoBehaviour
{
    public static int enemyCount = 0;
    int enemyIndex = 0;
    public GameObject[] enemyTypes; //0 = test, 1 = nerv, 2 = scare, 3= anger\
    public Vector2[] WavePositions;
    public int[] enemiesInWave;
    public int[] numbersInWaves;
    public TextMeshProUGUI textbox;
    //public GameObject textboxObj;
    public string[] waveDialog;
    int wave = 0;
    public combatplayer player;
    bool WaveSpawned = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //textbox = textboxObj.GetComponent<TextMeshPro>();
        textbox.text = waveDialog[wave];
        player.resetForWave();
        player.manager = this;
    }
    
    void spawn()
    {
        for(int i = 0; i < numbersInWaves[wave]; i++)
        {
            enemyCount++;
            var tmpEnemy = Instantiate(enemyTypes[enemiesInWave[enemyIndex]], WavePositions[enemyIndex], Quaternion.identity);
            enemyIndex++;
       
        }
        WaveSpawned = false;
    }
    public void spawnWave()
    {
        if (wave < numbersInWaves.Length)
        {
            spawn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyCount == 0 && !WaveSpawned)
        {
          
            wave++;
            if (wave < numbersInWaves.Length)
            {
                textbox.text = waveDialog[wave];
            }
            var bullets = GameObject.FindGameObjectsWithTag("damages");
            foreach (GameObject b in bullets)
            {
                Destroy(b);
            }
            player.resetForWave();
            WaveSpawned = true;

        }
    }
}
