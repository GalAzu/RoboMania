using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public HealthBar healthBar;
    private SpawnManager spawnManager;
    public TextMeshProUGUI EnemyCountText;
    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI playerState;
    public TextMeshProUGUI enemyKilledText;
    public TextMeshProUGUI timeToNextWaveText;
    public TextMeshProUGUI machineParts;
    public TextMeshProUGUI HP;
    public static UIManager instance;
    public GameObject nextWavePanel;
    public Canvas uiCanvas;
    public GameObject GameOverUI;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        healthBar = FindObjectOfType<HealthBar>();
        instance = this;
        uiCanvas.gameObject.SetActive(true);
    }
    private void Start()
    {
        UpdateEnemyCount();
        UpdateWaveNumber();
        UpdateMachineParts();
    }
    public void UpdateEnemyCount()
    {
        EnemyCountText.text = "Enemy Count:" + spawnManager.EnemyCount;
    }
    public void UpdateWaveNumber()
    {
        waveNumber.text = "Wave Number :" + spawnManager.waveNumber.ToString();
    }
    public void UpdateEnemyKilled()
    {
        enemyKilledText.text = "Enemy Killed : " + GameManager.instance.enemiesDefeated.ToString();
    }
    public void UpdateHP()
    {
        healthBar.SetSize(Character.instance.curHealth / 100);
        HP.text = "HP: " + Character.instance.curHealth;
        if (Character.instance.curHealth <= 0) GameManager.instance.GameOver();
    }
    public void UpdateMachineParts()
    {
        machineParts.text = "Machine Parts: " + Character.instance.machineParts;
    }
    public void GetCurrentStore()
    {
        //player choose random store
        //get store script from the triggered object
    }
}
