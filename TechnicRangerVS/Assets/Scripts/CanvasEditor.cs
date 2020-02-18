using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEditor : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject SecondCanvas;
    public GameObject ThirdCanvas;

    public void Editor()

    {
        if (FirstCanvas.gameObject.activeInHierarchy == false)
        {
            FirstCanvas.gameObject.SetActive(true);
            SecondCanvas.gameObject.SetActive(false);
            ThirdCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            FirstCanvas.gameObject.SetActive(false);
            SecondCanvas.gameObject.SetActive(true);
            ThirdCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        else
            {
                FirstCanvas.gameObject.SetActive(false);
                SecondCanvas.gameObject.SetActive(false);
                ThirdCanvas.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
       
        }
}
}
