using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorAnimation : MonoBehaviour
{

    public GameObject MovingPlatform;
    public GameObject Player;
    /*

    private void OnTriggerEnter(Collider other)
        {


            MovingPlatform.GetComponent<Animation>().enabled = true;
            Debug.Log("Hit");

    }
    */
    // child player to platfrom
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }
    // unchild player to platfrom
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }

    public void OnRayHit ()
    {
        MovingPlatform.GetComponent<Animation>().enabled = true;
    }

}
