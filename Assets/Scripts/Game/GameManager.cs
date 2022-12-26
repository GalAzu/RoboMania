using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UnityEvent enemiesDefeatedEvent;
    private Character character;
    public int enemiesDefeated;
    public bool storeIsOpened;
    private void Awake()
    {
        character = FindObjectOfType<Character>();
    }
    private void Start()
    {
        UIManager.instance.uiCanvas.gameObject.SetActive(true);
        storeIsOpened = false;
        instance = this;
        UIManager.instance.UpdateMachineParts();
        UIManager.instance.GameOverUI.SetActive(false);
    }
    public void GameOver()
    {
            character.curHealth = 0;
            Destroy(character.gameObject);
            Time.timeScale = 0;
            UIManager.instance.GameOverUI.SetActive(true);
          //  UIManager.instance.uiCanvas.gameObject.SetActive(false);

            //GAME OVER ANIMATION SEQUENCE
    }
    public void AddScoreToEnemiesDefeated()
    {
        character.machineParts += Random.Range(1, 5);
        enemiesDefeated++;
        UIManager.instance.UpdateEnemyKilled();
        UIManager.instance.UpdateMachineParts();
    }
}
