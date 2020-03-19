using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaskSwap : MonoBehaviour
{


    public PlayerController PlayerController;

    public GameObject AnchorGhost;
    public GameObject AnchorMask;

    public GameObject VistaGhost;
    public GameObject VistaMask;

    public GameObject ShieldGhost;
    public GameObject ShieldMask;

    public RawImage CurrentMask;

    public Texture DefaultTexture;
    public Texture AnchorTexture;
    public Texture VistaTexture;
    public Texture ShieldTexture;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.CurrentWeaponState == WeaponState.Default)
        {
            CurrentMask.texture = DefaultTexture;
        }
        else if (PlayerController.CurrentWeaponState == WeaponState.Anchor)
        {
            CurrentMask.texture = AnchorTexture;
        }
        else if (PlayerController.CurrentWeaponState == WeaponState.Vista)
        {
            CurrentMask.texture = VistaTexture;
        }
        else if (PlayerController.CurrentWeaponState == WeaponState.Shield)
        {
            CurrentMask.texture = ShieldTexture;
        }

        //ANCHOR
        if (PlayerController.GetUnlockedWeapons().Contains(WeaponState.Anchor))
        {
            AnchorGhost.SetActive(false);
            AnchorMask.SetActive(true);
        }
        else
        {
            AnchorGhost.SetActive(true);
            AnchorMask.SetActive(false);
        }


        //VISTA
        if (PlayerController.GetUnlockedWeapons().Contains(WeaponState.Vista))
        {
            VistaGhost.SetActive(false);
            VistaMask.SetActive(true);
        }
        else
        {
            VistaGhost.SetActive(true);
            VistaMask.SetActive(false);
        }


        //SHIELD
        if (PlayerController.GetUnlockedWeapons().Contains(WeaponState.Shield))
        {
            ShieldGhost.SetActive(false);
            ShieldMask.SetActive(true);
        }
        else
        {
            ShieldGhost.SetActive(true);
            ShieldMask.SetActive(false);
        }
    }
}
