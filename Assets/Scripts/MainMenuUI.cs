using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartButton()
    {
        Debug.Log("Start Button Works fucking fuck");
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void OnSettingButton()
    {
        Debug.Log("Setting Button Works fucking fuck");
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Exit Button Works fucking fuck");
        SceneManager.LoadScene(0);
    }
}
