using System.Collections;
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
       //if (MovingPlatform.GetComponent<SidetoSideMover>().enabled == false)
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

    public void Update()
    {
        if (MovingPlatform.GetComponent<SidetoSideMover>().enabled == false)
            Render.sharedMaterial = activeMat[0];
        else if (MovingPlatform.GetComponent<SidetoSideMover>().enabled == true)
            Render.sharedMaterial = activeMat[1];
    }

    public void OnRayHit ()
    {
              // idea for how to make it so the anchor can hold the platfroms in place but for some reason it causes the platfroms to sometimes stop and sometimes stop and instantly continue with a single click

       if (MovingPlatform.GetComponent<SidetoSideMover>().enabled == false)
        { 
            // the animation is bugged so im using the mover script for now
            //  MovingPlatform.GetComponent<Animation>().enabled = true;
        MovingPlatform.GetComponent<SidetoSideMover>().enabled = true;
        Render.sharedMaterial = activeMat[1];

        // change the materical to the activated form of the platfrom

        //if (MovingPlatform.GetComponent<SidetoSideMover>().enabled == true)
        //Render.sharedMaterial = activeMat[1];
        }

           else if(MovingPlatform.GetComponent<SidetoSideMover>().enabled == true)
           {
               MovingPlatform.GetComponent<SidetoSideMover>().enabled = false;
            Render.sharedMaterial = activeMat[0];
           }

          





    }

   
}
