using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class SpawnManager : MonoBehaviour
{
    public float timeToNextSpawn;
    [SerializeField]
    public int EnemyCount;
    [ShowInInspector]
    public static Enemy[] EnemiesLeft;
    public int waveNumber;
    public GameObject[] EnemiesPool;
    public int waveSize;
    public bool waveIsSpawning;
    public bool timerIsOn;
    public Shop store;
    public float secToSpawn;
    [SerializeField]
    private GameObject inventoryPanel;
    private Shop shop;
    [SerializeField]
    private List <Vector3> nextSpawnPoints = new();
    [Title("VFX")]
    [SerializeField]
    private GameObject spawnEffect;
    [SerializeField]
    private GameObject onSpawnEffect;

    private void Awake()
    {
        store = FindObjectOfType<Shop>();
        waveSize = 3;
        waveNumber = 0;
        waveIsSpawning = false;
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (waveIsSpawning == true)
        {
            Timer();
        }
        if (EnemyCount == 0 && waveIsSpawning == false)
        {
            waveIsSpawning = true;
            StartCoroutine(WaveSpawn());
            Debug.Log("WaveSpawn");
        }
    }
    private IEnumerator WaveSpawn()
    {
        Character.instance.waitForSpawn = true;
        UpdateSpawnPoints();
        ShowEnemiesLocation();
        yield return new WaitForSeconds(timeToNextSpawn);
        {
            SpawnEnemies();
            inventoryPanel.gameObject.SetActive(true);
            EnemiesOnLevel();
            waveSize += Random.Range(1, 3);
            waveNumber++;
            waveIsSpawning = false;
            Character.instance.waitForSpawn = false;
            UIManager.instance.storeUI.gameObject.SetActive(false);
            UIManager.instance.nextWavePanel.SetActive(false);
            UIManager.instance.UpdateWaveNumber();
            UIManager.instance.UpdateEnemyCount();
        }
    }

    private void Timer()
    {
        inventoryPanel.gameObject.SetActive(false);
        UIManager.instance.nextWavePanel.SetActive(true);
        timeToNextSpawn -= Time.deltaTime;
        UIManager.instance.timeToNextWaveText.text = "Next wave will start in: " + ((int)timeToNextSpawn).ToString();
        if (timeToNextSpawn <= 0) timeToNextSpawn = secToSpawn;
    }
    private void SpawnEnemies()
    {
        foreach(Vector3 spawnPoint in nextSpawnPoints)
        {
            int randomEnemyIndex = Random.Range(0, EnemiesPool.Length);
            var newEnemy = Instantiate(EnemiesPool[randomEnemyIndex], spawnPoint , Quaternion.identity);
            var spawnfx = Instantiate(onSpawnEffect, spawnPoint, Quaternion.identity);
            Destroy(spawnfx, 1);
        }
    }
    private void UpdateSpawnPoints()
    {
        for(int enemyInWave=0; enemyInWave <= waveSize; enemyInWave++)
            nextSpawnPoints.Add(NewSpawnPos());
        
    }
    private Vector3 NewSpawnPos()
    {
        var newPos = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30));
        return newPos;
    }
    private void EnemiesOnLevel()
    {
        EnemiesLeft = GameObject.FindObjectsOfType<Enemy>();
        EnemyCount = EnemiesLeft.Length;
    }
    private void ShowEnemiesLocation()
    {
        foreach(Vector3 spawnPoint in nextSpawnPoints)
        {
            GameObject indicator = Instantiate(spawnEffect, spawnPoint, Quaternion.identity);
            Destroy(indicator , timeToNextSpawn);
        }
    }

}
