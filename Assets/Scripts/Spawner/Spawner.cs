using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Wave Settings
    public int waveNumber = 0;
    public int enemySpawnAmount = 0;
    public int enemiesKilled = 0;
    public static int enemiesKilledCurrently = 0;
    public int enemiesLeft;
    public static int enemiesLeftCurrently = 0;

    //Spawners
    public GameObject[] spawners;
    public GameObject bossSpawner;

    //Enemies
    public GameObject[] enemy;
    public GameObject boss;

    //Box Collider to start waves
    public BoxCollider boxCollider;

    //Timer
    public float timeLeft;
    public bool timerOn;

    //Name Panels
    public GameObject fruitKingdomName;
    public GameObject dairyKingdomName;


    private void Start()
    {
        spawners = new GameObject[spawners.Length];

        for(int i = 0; i < spawners.Length; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;
        }

        timerOn = false;

        fruitKingdomName.SetActive(false);
        dairyKingdomName.SetActive(false);
    }

    private void Update()
    {
        if (timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timerOn = false;
            }
        }
        else
        {
            timeLeft = 2.0f;
            UpdateTimer(timeLeft);
        }

        if(waveNumber >= 1 && waveNumber != 4 && enemiesLeftCurrently == 0)
        {
            timerOn = true;

            if (timeLeft <= 0)
            {
                NextWave();
                if (waveNumber == 4)
                {
                    enemiesLeftCurrently += 1;
                    BossWave();
                }
            }
        }

        enemiesLeft = enemiesLeftCurrently;
        enemiesKilled = enemiesKilledCurrently;
    }

    private void SpawnBoss()
    {
        Instantiate(boss, bossSpawner.transform.position, bossSpawner.transform.rotation);
    }

    private void SpawnEnemy()
    {
        int spawnerID = Random.Range(0, spawners.Length);
        int enemySpawn = Random.Range(0, enemy.Length);
        Instantiate(enemy[enemySpawn], spawners[spawnerID].transform.position, spawners[spawnerID].transform.rotation);
    }

    private void StartWave()
    {
        waveNumber = 1;
        enemySpawnAmount = 2;
        enemiesKilledCurrently = 0;
        enemiesLeftCurrently = enemySpawnAmount;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void NextWave()
    {
        waveNumber++;
        enemySpawnAmount += 2;
        enemiesKilledCurrently = 0;
        enemiesLeftCurrently = enemySpawnAmount;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            SpawnEnemy();
        }
    }

    private void BossWave()
    {
        SpawnBoss();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartWave();
            waveNumber = 1;
            boxCollider.enabled = false;

            if(gameObject.tag == "FruitKingdom")
            {
                fruitKingdomName.SetActive(true);
                StartCoroutine(CloseKingdomName());
            }

            if(gameObject.tag == "DairyKingdom")
            {
                dairyKingdomName.SetActive(true);
                StartCoroutine(CloseKingdomName());
            }
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);
    }

    private IEnumerator CloseKingdomName()
    {
        yield return new WaitForSeconds(3.0f);
        fruitKingdomName.SetActive(false);
        dairyKingdomName.SetActive(false);
    }

}
