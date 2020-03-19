using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    //For Weapon/Ability/Mask pick-ups (also see MaskPickup script)
    //USE LIST insead of ARRAY because a list CAN GROW, Array can not (can add and contain in a list)
    public List<WeaponState> UnlockedWeapons;

    public static PersistentData Instance { get { return _instance; } }

    private static PersistentData _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
