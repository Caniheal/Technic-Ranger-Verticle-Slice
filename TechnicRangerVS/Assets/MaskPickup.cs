using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public WeaponState weaponUnlock;

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController Controller = other.GetComponent<PlayerController>();
        if (Controller)
        {
            Controller.UnlockWeapon(weaponUnlock);
        }
    }
}
