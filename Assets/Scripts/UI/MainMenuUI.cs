using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
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
