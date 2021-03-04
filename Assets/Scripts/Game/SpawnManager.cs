using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public float timeToNextSpawn;
    [SerializeField]
    public int EnemyCount;
    public static Enemy[] EnemiesLeft;
    public int waveNumber;
    public GameObject[] MultipleEnemiesToSpawn;
    public GameObject SpiderEnemy;
    public int waveSize;
    public bool waveSpawned;
    public bool timerIsOn;
    public Shop store;

    private void Awake()
    {
        store = FindObjectOfType<Shop>();
        waveSize = 3;
        instance = this;
        waveNumber = 0;
        waveSpawned = false;
    }
    private void Update()
    {
        if(waveSpawned == true)
        {
            Timer();
        }
        if (EnemyCount == 0 && waveSpawned == false)
        {
            waveSpawned = true;
            Character.instance.waitForSpawn = true;
            StartCoroutine(WaveSpawn());
        }
    }
    private IEnumerator WaveSpawn()
    {
        if(store != null)
        {
            store.CreateNewItemSlots();
            store.UpdateShopLists();
        }
        yield return new WaitForSeconds(timeToNextSpawn);
        {
            for (int i = 0; i < waveSize; i++)
            {
                SpawnEnemies();
            }
            UIManager.instance.nextWavePanel.SetActive(false);
            EnemiesOnLevel();
            waveSize += Random.Range(1, 3);
            waveNumber++;
            Character.instance.waitForSpawn = false;
            UIManager.instance.store.gameObject.SetActive(false);
            UIManager.instance.UpdateWaveNumber();
            UIManager.instance.UpdateEnemyCount();
            waveSpawned = false;
        }
    }
    private void Timer()
    {
        UIManager.instance.nextWavePanel.SetActive(true);
        timeToNextSpawn -= Time.deltaTime;
        UIManager.instance.timeToNextWaveText.text = "Next wave will start in: " + ((int)timeToNextSpawn).ToString();
        if (timeToNextSpawn <= 0) timeToNextSpawn = 16;
    }
    private void SpawnEnemies()
    {
        var newEnemy = Instantiate(SpiderEnemy, NewSpawnPos(), Quaternion.identity);
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
}
