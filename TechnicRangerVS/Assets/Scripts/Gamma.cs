using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamma : MonoBehaviour
{
    float rgbValue = .5f;
    void OnGUI()
    {
        rgbValue = GUI.HorizontalSlider (new Rect (Screen.width/3 - 200,275,100,25), rgbValue, 0f,1.0f);
        RenderSettings.ambientLight = new Color(rgbValue, rgbValue, rgbValue, 1);
    }
}
