using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gamma : MonoBehaviour
{

    public float GammaCorrection;


    void Update()
    {

        RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);

    }

    void OnGUI()
    {


    }

}
