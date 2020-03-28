﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B Button"))
        {
            TaskOnClick();
        }
    }
    public void TaskOnClick()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
