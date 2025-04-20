using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class combatmanager : MonoBehaviour
{
    public static int enemyCount = 0;
    int enemyIndex = 0;
    public GameObject[] enemyTypes; //0 = sad, 1 = anxious, 2 = scare, 3= anger
    public Vector2[] WavePositions;
    public int[] enemiesInWave;
    public int[] numbersInWaves;
    public TextMeshProUGUI textbox;
    //public GameObject textboxObj;
    public string[] waveDialog;
    int wave = 0;
    public combatplayer player;
    bool WaveSpawned = true;
    public GameObject clickPrompt;
    public GameObject[] hideOnGameOver;
    public GameObject[] showOnGameOver;
    public string nextScene;
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI combatName;
    public string[] names;
    public GameObject wintext;
    public Sprite[] waveImages;
    SpriteRenderer sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void updateWaveTextImage()
    {
        if (waveCounter != null)
        {
            waveCounter.text = "Wave: " + (wave + 1) + "/" + numbersInWaves.Length;
        }
        if (wave < numbersInWaves.Length) //needs to be 1 more than the rest of the arrays bcs of closing dialog
        {
            if(combatName != null)
            {
                combatName.text = names[wave];
            }
            textbox.text = waveDialog[wave];
            sp.sprite = waveImages[wave];
        }
        else if (wave == numbersInWaves.Length)
        {
            if (combatName != null)
            {
                combatName.text = names[wave];
            }
            textbox.text = waveDialog[wave];
            waveCounter.enabled = false;
            wintext.SetActive(true);
            sp.sprite = waveImages[wave];

        }
    }
    public void gameOver()
    {
        player.pivot.SetActive(false);
        Time.timeScale = 0;
        foreach (GameObject go in hideOnGameOver) { 
            go.SetActive(false);
        }
        foreach (GameObject go in showOnGameOver)
        {
            go.SetActive(true);
        }
    }
    public void Reset()
    {
        wave = 0;
        enemyCount = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Start()
    {
        
        
        //textbox = textboxObj.GetComponent<TextMeshPro>();
        player.resetForWave();
        player.manager = this;
        sp = GetComponent<SpriteRenderer>();
        updateWaveTextImage();
        clickPrompt.SetActive(true);
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
        clickPrompt.SetActive(false);
        if (wave < numbersInWaves.Length)
        {
            spawn();
        }else if (wave == numbersInWaves.Length)
        {

            SceneManager.LoadScene(nextScene);

        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(wave + " "+enemyCount);
        if(enemyCount == 0 && !WaveSpawned)
        {
          
            wave++;
            updateWaveTextImage();


            var bullets = GameObject.FindGameObjectsWithTag("damages");
            foreach (GameObject b in bullets)
            {
                Destroy(b);
            }
            clickPrompt.SetActive(true);
            player.resetForWave();
            WaveSpawned = true;

        }
    }
}
