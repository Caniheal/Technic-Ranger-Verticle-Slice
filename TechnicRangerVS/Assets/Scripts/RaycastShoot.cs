using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public Camera AnchorCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Shoot();
        }
    }


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
            */
        }
    }
}
