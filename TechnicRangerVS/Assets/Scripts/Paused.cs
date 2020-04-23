using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SceneSwitching;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.EventSystems;

public class Paused : MonoBehaviour
{
    private static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject trophiesMenuUI;
    public Button Back;
    PlayerController coin;
    public static string fileName = Application.streamingAssetsPath + "/" + fileBackup;
    public static string fileBackup = "SaveFile.json";

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
        if (btn)
        {
            btn.onClick.AddListener(TaskOnClick);
        }
        //btn.onClick.AddListener(SecondTaskOnClick);
        GameIsPaused = false;
        Image1.enabled = false;
        Image2.enabled = false;
        Image3.enabled = false;
        Image4.enabled = false;
        Image5.enabled = false;
        Image6.enabled = false;
        Image7.enabled = false;
        Image8.enabled = false;
        coin = GameObject.Find("Prefab_FinalCharacterModel").GetComponent<PlayerController>();// need this game object to access count

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
        if (!GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            AudioListener.pause = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        trophiesMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioListener.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Save(coin.count);
    }

    public void Trophies()
    {
        
        pauseMenuUI.SetActive(false);
        trophiesMenuUI.SetActive(true);
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
    
    public static void Save(int coinNum)
    {
        /*if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }*/
        // Creates file if it doesnt exist
        //if (!File.Exists(fileName))

        fileName = Application.streamingAssetsPath + "/" + fileBackup;
        if (!File.Exists(fileName))
        {
            File.Create(fileName).Close();
        }

        // Saving a game file
        List<SaveData> _data = new List<SaveData>();
        _data.Add(new SaveData()
        {
            coinCount = coinNum
        });
        string json = JsonConvert.SerializeObject(_data.ToArray());

        //write string to file
        File.WriteAllText(fileName, json);


    }

    public void TaskOnClick()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
    }

}
