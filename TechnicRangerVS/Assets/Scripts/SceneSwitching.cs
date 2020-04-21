using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using static Paused;

public class SceneSwitching : MonoBehaviour
{
    //Paused paused;
    public GameObject pausedInstanceObject;
    public Paused pause;
    public class SaveData
    {
        public int coinCount { get; set; }
    }
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pause = pausedInstanceObject.GetComponent<Paused>();
    }
    
    public void StartGame()
    {
        //paused = new Paused();
        //saving and ititilizing the number to 0 if it doesnt existe
        Paused.Save(0);
        SceneManager.LoadScene("IntroCutscene");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits Menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
