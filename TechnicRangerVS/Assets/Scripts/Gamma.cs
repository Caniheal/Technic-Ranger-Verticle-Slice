using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamma : MonoBehaviour
{
    public float rgbValue = .5f;

    // Update is called once per frame
    public void ChangeLight(float rgbGamma)
    {
        rgbValue = rgbGamma;
        RenderSettings.ambientLight = new Color(rgbValue, rgbValue, rgbValue, 1);
    }
}
