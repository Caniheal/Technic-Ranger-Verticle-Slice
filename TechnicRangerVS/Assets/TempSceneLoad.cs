using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneLoad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "MainCharacter2.0 1")
        {
            SceneManager.LoadScene("Main Scene");
        }
    }
}
