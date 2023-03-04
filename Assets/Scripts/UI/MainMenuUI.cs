using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private int startingSceneNumber = 1;
    public void OnStartButton()
    {
        SceneManager.LoadScene(startingSceneNumber);
        Time.timeScale = 1;
    }

    public void OnSettingButton()
    {
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
