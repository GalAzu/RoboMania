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
    public int enemyCount;

    private void Awake()
    {
        instance = this;

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        UIManager.instance.uiCanvas.gameObject.SetActive(true);
        UIManager.instance.GameOverUI.SetActive(false);
    }
    private void Start()
    {
        UIManager.instance.UpdateMachineParts();

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
    public void EnemiesOnLevel()
    {
        GameObject[] EnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy"); //TODO Refactor for better performance, Get local list of enemies from each enemy?
        enemyCount = EnemiesLeft.Length;
    }
}
