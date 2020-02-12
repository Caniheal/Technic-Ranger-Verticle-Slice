using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorAnimation : MonoBehaviour
{
    
    public GameObject Projectile;
    public GameObject MovingPlatform;
    /*

    private void OnTriggerEnter(Collider other)
        {


            MovingPlatform.GetComponent<Animation>().enabled = true;
            Debug.Log("Hit");

    }
    */

    public void OnRayHit ()
    {
        MovingPlatform.GetComponent<Animation>().enabled = true;
    }



}
