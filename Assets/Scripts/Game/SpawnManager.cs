using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using _Utilities;

public class SpawnManager : MonoBehaviour
{
    public float timeToNextSpawn;
    [SerializeField]
    [ShowInInspector]
    public int waveNumber;
    public GameObject[] EnemiesInScene;
    public int waveSize;
    public bool waveIsSpawning;
    public bool timerIsOn;
    public float secToSpawn;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private List <Vector2> nextSpawnPoints = new();
    [Title("VFX")]
    [SerializeField]
    private GameObject spawnEffect;
    [SerializeField]
    private GameObject onSpawnEffect;
    private CameraFollow mainCamera;

    private void Awake()
    {
        waveSize = 3;
        waveNumber = 0;
        waveIsSpawning = false;
        mainCamera = Camera.main.GetComponent<CameraFollow>();
    }
    private void Update()
    {
        if (waveIsSpawning == true)
        {
            Timer();
        }
        if (GameManager.instance.enemyCount == 0 && waveIsSpawning == false)
        {
            waveIsSpawning = true;
            StartCoroutine(StartSpawn());
        }
    }
    private IEnumerator StartSpawn()
    {
        Character.instance.waitForSpawn = true;
        UpdateSpawnPoints();
        ShowEnemiesLocation();
        yield return new WaitForSeconds(timeToNextSpawn);
        {
            SpawnEnemies();
            inventoryPanel.gameObject.SetActive(true);
            GameManager.instance.EnemiesOnLevel();
            waveSize += Random.Range(1, 3);
            waveNumber++;
            waveIsSpawning = false;
            Character.instance.waitForSpawn = false;
            UIManager.instance.nextWavePanel.SetActive(false);
            UIManager.instance.UpdateWaveNumber();
            UIManager.instance.UpdateEnemyCount();
            //TODO Refactor this whole mess into OnSpawn event
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
            int randomEnemyIndex = Random.Range(0, EnemiesInScene.Length);
            var newEnemy = Instantiate(EnemiesInScene[randomEnemyIndex], spawnPoint , Quaternion.identity);
            var spawnfx = Instantiate(onSpawnEffect, spawnPoint, Quaternion.identity);
            Destroy(spawnfx, 1);
        }
    }
    private void UpdateSpawnPoints()
    {
        for (int enemyInWave=0; enemyInWave <= waveSize; enemyInWave++)
        {
            Vector2 nextSpawnPos = Vector2Utilities.NewSpawnPos(mainCamera.x_minVal, mainCamera.x_maxVal, mainCamera.y_minVal, mainCamera.y_maxVal);
            nextSpawnPoints.Add(nextSpawnPos);
        }
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
