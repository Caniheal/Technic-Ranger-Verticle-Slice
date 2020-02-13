using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public Camera AnchorCam;

    // Update is called once per frame
    void Update()
    {
       
      
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetButtonDown("rightTrigger"))
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(AnchorCam.transform.position, AnchorCam.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    var anchorAnimation = hit.collider.gameObject.GetComponent<AnchorAnimation>();
                    if (anchorAnimation != null)
                    {
                        anchorAnimation.OnRayHit();
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                        Debug.Log("Did Hit");
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        
    }

}

/*
    void Shoot()
    {
       
        RaycastHit hit;
        if (Physics.Raycast(AnchorCam.transform.position, AnchorCam.transform.forward))
        {
            Debug.Log(".transform.name");

            /* Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.GetComponent<Animation>().enabled = true;
            } 
            
        }
    }
} */
