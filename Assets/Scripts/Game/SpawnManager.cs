using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public float timeToNextSpawn;
    [SerializeField]
    public int EnemyCount;
    public static Enemy[] EnemiesLeft;
    public int waveNumber;
    public GameObject[] MultipleEnemiesToSpawn;
    public int waveSize;
    public bool waveIsSpawning;
    public bool timerIsOn;
    public Shop store;
    public float secToSpawn;
    [SerializeField]
    private GameObject inventoryPanel;
    private Shop shop;
    private void Awake()
    {
        store = FindObjectOfType<Shop>();
        waveSize = 3;
        waveNumber = 0;
        waveIsSpawning = false;
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
        }
    }
    private IEnumerator WaveSpawn()
    {
        Character.instance.waitForSpawn = true;
        yield return new WaitForSeconds(timeToNextSpawn);
        {
            for (int i = 0; i < waveSize; i++)
            {
                SpawnEnemies();

            }
            UIManager.instance.nextWavePanel.SetActive(false);
            inventoryPanel.gameObject.SetActive(true);
            EnemiesOnLevel();
            waveSize += Random.Range(1, 3);
            waveNumber++;
            UIManager.instance.storeUI.gameObject.SetActive(false);
            UIManager.instance.UpdateWaveNumber();
            UIManager.instance.UpdateEnemyCount();
            waveIsSpawning = false;
            Character.instance.waitForSpawn = false;
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
        var randomEnemy = Random.Range(0, MultipleEnemiesToSpawn.Length);
        var newEnemy = Instantiate(MultipleEnemiesToSpawn[randomEnemy], NewSpawnPos(), Quaternion.identity);
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
