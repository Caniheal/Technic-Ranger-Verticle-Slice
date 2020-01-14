using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void FinishGame()
    {
        SceneManager.LoadScene("Menu");

    }

    public void StartGame()
    {
        SceneManager.LoadScene("backup2");

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test2");

        if (other.gameObject.tag == ("Player"))
        {
            Invoke("FinishGame", 5);

            Debug.Log("test");
        }

    }
}
