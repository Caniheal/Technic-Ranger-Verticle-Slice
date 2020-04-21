using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class SceneSwitching : MonoBehaviour
{
    public class SaveData
    {
        public int coinCount { get; set; }
    }
    private void Start()
    {

        if (!File.Exists("SaveFile.json"))
        {
            // Saving a game file
            List<SaveData> _data = new List<SaveData>();
            _data.Add(new SaveData()
            {
                coinCount = 0
            });
            string json = JsonConvert.SerializeObject(_data.ToArray());
            //write string to file
            System.IO.File.WriteAllText(@"SaveFile.json", json);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {

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
