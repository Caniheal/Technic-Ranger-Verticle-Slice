using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    public Material Purple, Teal, Tan, Blue, White;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material = Tan;
    }

    public void UpdateColors(WeaponState CurrentWeaponState)
    {
        Debug.Log("Test");

        if (CurrentWeaponState == WeaponState.Default)
        {
            GetComponent<Renderer>().material = Tan;
        }
        if (CurrentWeaponState == WeaponState.Vista)
        {
            //VISTA
            GetComponent<Renderer>().material = Purple;
        }
        if (CurrentWeaponState == WeaponState.Anchor)
        {
            //ANCHOR
            GetComponent<Renderer>().material = Teal;
        }
        if (CurrentWeaponState == WeaponState.Shield)
        {
            //SHIELD
            GetComponent<Renderer>().material = White;
        }
       // if (Input.GetKeyDown("5"))
        {
           // GetComponent<Renderer>().material = Blue;
        }
    }
}
