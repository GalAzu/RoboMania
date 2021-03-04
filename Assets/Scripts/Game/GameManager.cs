using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent enemiesDefeatedEvent;
    private Character player;
    public int MachineParts;
    public int enemiesDefeated;
    public bool storeIsOpened;
    private void Start()
    {
        storeIsOpened = false;
        instance = this;
        player = Character.instance;
        UIManager.instance.UpdateMachineParts();
    }
    public void GameOver()
    {
        if(player.curHealth <= 0 )
        {
            player.curHealth = 0;
            Destroy(player.gameObject);
            Time.timeScale = 0;
            //GAME OVER SEQUENCE
            //GAME OVER UI
        }
    }
    public void AddScoreToEnemiesDefeated()
    {
        MachineParts +=Random.Range(1,5);
        enemiesDefeated++;
        UIManager.instance.UpdateEnemyKilled();
        UIManager.instance.UpdateMachineParts();
    }
}
