using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
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
    public Shop store;

    private void Awake()
    {
        instance = this;
        uiCanvas.gameObject.SetActive(true);
        store = FindObjectOfType<Shop>();
        OpenAndCloseStore();
    }
    private void Start()
    {
        UpdateEnemyCount();
        UpdateWaveNumber();
        UpdateHP();
        UpdateMachineParts();
    }
    public void OpenAndCloseStore()
    {
        if (store.gameObject.activeSelf == false)
           store.gameObject.SetActive(true);
        else store.gameObject.SetActive(false);
    }
    public void UpdateEnemyCount()
    {
        EnemyCountText.text = "Enemy Count:" + SpawnManager.instance.EnemyCount;
    }
    public void UpdateWaveNumber()
    {
        waveNumber.text = "Wave Number :" + SpawnManager.instance.waveNumber.ToString();
    }
    public void UpdateEnemyKilled()
    {
        enemyKilledText.text = "Enemy Killed : " + GameManager.instance.enemiesDefeated.ToString();
    }
    public void UpdateHP()
    {
        HP.text = "HP: " + Character.instance.curHealth;
    }
    public void UpdateMachineParts()
    {
       machineParts.text = "Machine Parts: " + Character.instance.machineParts;
    }
}
