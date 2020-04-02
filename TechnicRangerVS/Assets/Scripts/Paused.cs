﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    private static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject trophiesMenuUI;
    public Button Back;
    public Button TButton;

    void Start()
    {
        Button btn = Back.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //btn.onClick.AddListener(SecondTaskOnClick);
        pauseMenuUI.SetActive(false);

    }
        
        // Update is called once per frame
        void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume(); 
            }
            else
            {
                Pause();
            }
        }
       else if (GameIsPaused && settingsMenuUI.activeInHierarchy)
        {
            Settings();
        }
        if (!GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
        }

        if (GameIsPaused && settingsMenuUI == isActiveAndEnabled && Input.GetButtonDown("B Button"))
        {
            TaskOnClick();
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(false);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        trophiesMenuUI.SetActive(false);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Trophies()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    void TaskOnClick()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        
    }

}
