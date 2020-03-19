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
        Debug.Log("CollectedObject");
        PlayerController Controller = other.GetComponent<PlayerController>();
        if (Controller)
        {
            if (sound)
            {
                sound.Play();
            }

            Controller.UnlockWeapon(weaponUnlock);
            Destroy(gameObject, .15f);

        }
    }
}
