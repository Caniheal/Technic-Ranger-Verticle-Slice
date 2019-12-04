using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookv2 : MonoBehaviour
{
    public Camera Camera;
    public GameObject HookPoint;
    public float AimDownSightsFOV;
    public float ShootDistance = 5;

    bool IsActive = false;
    float StartingFOV;

    void Start()
    {
        StartingFOV = Camera.fieldOfView;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            return;
        }

        //RMB - ADS (aim down sights)
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.fieldOfView = AimDownSightsFOV;
        }
        else
        {
            Camera.fieldOfView = StartingFOV;
        }

        //LMB - Shoot
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //grapping hook shoots from center of camera
            Vector3 CameraDirection = Camera.transform.rotation * Vector3.forward;

            Debug.DrawLine(Camera.transform.position, Camera.transform.position + CameraDirection * ShootDistance, Color.red, 30);

            //Shoots the grappling hook from the camera's position to the way you're facing (shoots for a certain distance)

            RaycastHit[] hits = Physics.RaycastAll(Camera.transform.position, CameraDirection, ShootDistance);

            foreach (RaycastHit hit in hits)
            {
                // Continue if the hit object is this gameobject.
               // if (hit.collider.gameObject == gameObject)
                 //   continue;

                Hook Hook = hit.collider.gameObject.GetComponent<Hook>();

                if (Hook)
                {
                    Hook.StartHook(HookPoint);
                }

                Debug.Log(hit.collider.name);

               // break;
            }
        }
    }
}
