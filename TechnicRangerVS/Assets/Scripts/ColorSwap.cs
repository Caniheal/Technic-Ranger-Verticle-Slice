using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    public Material Purple, Teal, Tan, Blue, White;
    public SkinnedMeshRenderer Render;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material = Tan;
    }

    public void UpdateColors(WeaponState CurrentWeaponState)
    {
        return;

        Material[] materials = Render.materials;
        Debug.Log(materials[0]);

        if (CurrentWeaponState == WeaponState.Default)
        {
            Debug.Log("Default Color");

            materials[0] = Tan;
        }
        if (CurrentWeaponState == WeaponState.Vista)
        {
            Debug.Log("vista color");

            //VISTA
            materials[0] = Purple;
        }
        if (CurrentWeaponState == WeaponState.Anchor)
        {
            Debug.Log("anchor color");

            //ANCHOR
            materials[0] = Teal;
        }
        if (CurrentWeaponState == WeaponState.Shield)
        {
            Debug.Log("shield color");

            //SHIELD
            materials[0] = White;
        }

        Render.materials = materials;

       // if (Input.GetKeyDown("5"))
        {
           // GetComponent<Renderer>().material = Blue;
        }
    }
}
