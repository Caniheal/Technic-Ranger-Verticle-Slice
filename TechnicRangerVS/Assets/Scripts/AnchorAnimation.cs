﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnchorAnimation : MonoBehaviour
{

    public GameObject MovingPlatform;
    public GameObject Player;
    // color swaping references
    public Material[] activeMat;
    public SkinnedMeshRenderer Render;

    private void Start()
    {
       
        Render.sharedMaterial = activeMat[0];
    }


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
        
        // the animation is bugged so im using the mover script for now
        //  MovingPlatform.GetComponent<Animation>().enabled = true;
        MovingPlatform.GetComponent<SidetoSideMover>().enabled = true;

            // change the materical to the activated form of the platfrom

            Render.sharedMaterial = activeMat[1];





        }

   
}
