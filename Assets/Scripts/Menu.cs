using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    public GameObject pauseMenuUI;
    public GameObject btnPause;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        btnPause.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        btnPause.SetActive(false);
        Time.timeScale = 0f;
    }
}
