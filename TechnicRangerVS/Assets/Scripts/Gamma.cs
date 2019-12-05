using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamma : MonoBehaviour
{

    public GameObject GammaController;
    public Slider gamma;
    public float globalVolume;
    void Start()
    {
        gamma = GameObject.Find("Slider").GetComponent<Slider>();
        GammaController = GameObject.Find("ambientLight");
    }

    void OnGUI()
    {
        
    }
}
