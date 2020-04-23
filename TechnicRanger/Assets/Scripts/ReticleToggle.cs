using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleToggle : MonoBehaviour
{

    public Image img;

    // Start is called before the first frame update
    void Start()
    {
        img.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            img.enabled = true;
        }
        else
        {
            img.enabled = false;
        }
    }
}
