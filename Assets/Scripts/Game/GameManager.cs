using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent enemiesDefeatedEvent;
    private Character player;
    public int enemiesDefeated;
    public bool storeIsOpened;
    private void Start()
    {
        UIManager.instance.uiCanvas.gameObject.SetActive(true);
        storeIsOpened = false;
        instance = this;
        player = Character.instance;
        UIManager.instance.UpdateMachineParts();
        UIManager.instance.GameOverUI.SetActive(false);
    }
    public void GameOver()
    {
            Debug.Log("game fucking over");
            player.curHealth = 0;
            Destroy(player.gameObject);
            Time.timeScale = 0;
            UIManager.instance.GameOverUI.SetActive(true);
          //  UIManager.instance.uiCanvas.gameObject.SetActive(false);

            //GAME OVER ANIMATION SEQUENCE
    }
    public void AddScoreToEnemiesDefeated()
    {
        Character.instance.machineParts += Random.Range(1, 5);
        enemiesDefeated++;
        UIManager.instance.UpdateEnemyKilled();
        UIManager.instance.UpdateMachineParts();
    }
}
