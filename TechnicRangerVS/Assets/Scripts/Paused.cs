﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SceneSwitching;
using Newtonsoft.Json;

public class Paused : MonoBehaviour
{
    private static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject trophiesMenuUI;
    public Button Back;


    [SerializeField] public Image Image1;
    [SerializeField] public Image Image2;
    [SerializeField] public Image Image3;
    [SerializeField] public Image Image4;
    [SerializeField] public Image Image5;
    [SerializeField] public Image Image6;
    [SerializeField] public Image Image7;
    [SerializeField] public Image Image8;

    void Start()
    {
        Button btn = Back.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //btn.onClick.AddListener(SecondTaskOnClick);
        pauseMenuUI.SetActive(false);
        Image1.enabled = false;
        Image2.enabled = false;
        Image3.enabled = false;
        Image4.enabled = false;
        Image5.enabled = false;
        Image6.enabled = false;
        Image7.enabled = false;
        Image8.enabled = false;

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
        // Saving a game file
        List<SaveData> _data = new List<SaveData>();
        _data.Add(new SaveData()
        {
            coinCount = 0
        });
        string json = JsonConvert.SerializeObject(_data.ToArray());
        //write string to file
        System.IO.File.WriteAllText(@"SaveFile.json", json);

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
        
        //PlayerController coin = GameObject.Find("MainCharacter2.0 1").GetComponent<PlayerController>();
        PlayerController coin = GameObject.Find("Prefab_FinalCharacterModel").GetComponent<PlayerController>();
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (coin.count == 3)
        {
            Image1.enabled = true;
        }

        else if (coin.count == 12)
        {
            Image2.enabled = true;
        }
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
