using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    [SerializeField] private GameObject pauseMenuMainUI;

    [SerializeField] private GameObject pauseMenuOptionsUI;

    [SerializeField] private bool IsPaused;


    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        pauseMenuMainUI.SetActive(true);
        pauseMenuOptionsUI.SetActive(false);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        pauseMenuMainUI.SetActive(false);
        pauseMenuOptionsUI.SetActive(false);
    }

    public void ShowVolume()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuMainUI.SetActive(false);
        pauseMenuOptionsUI.SetActive(true);
    }


}

