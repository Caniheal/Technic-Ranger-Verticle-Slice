using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SceneSwitching;
using Newtonsoft.Json;
using System.IO;

public class Paused : MonoBehaviour
{
    private static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject trophiesMenuUI;
    public Button Back;
    PlayerController coin;
    public static string fileName = Application.streamingAssetsPath + "/" + fileBackup; //"C:/Users/Public/Documents/Tech/SaveFile.json";
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
        coin = GameObject.Find("Prefab_FinalCharacterModel").GetComponent<PlayerController>();

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
        Save(coin.count);
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
        string json = JsonConvert.SerializeObject(_data.ToArray()); // [{'coinCount': 0 }] 
        //write string to file
        File.WriteAllText(fileName, json);

        //FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
        //StreamWriter outputStream = new StreamWriter(fs);
        //outputStream.Write(json);

        //outputStream.Close();
        //fs.Close();
        //fs.Dispose();


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
